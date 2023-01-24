using System.Net;

namespace BoatSimulation
{
    internal class Program
    {
        private static int levegoHomerseklet = 20;
        private static Motorallapot aktualisMotorallapot = new Motorallapot() { AkkuToltoaram = 0, AkkuFeszultseg = 0, MotorAram = 0, MotorHomerseklet = levegoHomerseklet, AkkuHomerseklet = levegoHomerseklet, AbszolutSebesseg = 0, RelativSebesseg = 0, Fenyerosseg = 10 };
        private static Motorallapot minimumMotorallapot = new Motorallapot() { AkkuToltoaram = 0, AkkuFeszultseg = 0, MotorAram = 0, MotorHomerseklet = levegoHomerseklet, AkkuHomerseklet = levegoHomerseklet, AbszolutSebesseg = 0, RelativSebesseg = 0, Fenyerosseg = 0 };
        private static Motorallapot maximumMotorallapot = new Motorallapot() { AkkuToltoaram = 1000, AkkuFeszultseg = 500, MotorAram = 1000, MotorHomerseklet = 1000, AkkuHomerseklet = 1000, AbszolutSebesseg = 30, RelativSebesseg = 30, Fenyerosseg = 100 };
        private static Motorallapot maximumMotorallapotValtozas = new Motorallapot() { AkkuToltoaram = 2, AkkuFeszultseg = 10, MotorAram = 20, MotorHomerseklet = 2, AkkuHomerseklet = 2, AbszolutSebesseg = 1, RelativSebesseg = 1, Fenyerosseg = 1 };
        private static Random rnd = new Random();
        private static bool inUse = false;
        private static HttpClient client = new()
        {
            BaseAddress = new Uri("http://localhost:8080/api/values"),
        };

        private static async Task Main(string[] args)
        {
            Console.WriteLine("SolarBoat szimuláció");
            await TorlesKerdes();
            WaitingForTheStart();

            Timer t = new Timer(TimerCallBack, new object(), 0, 1000);

            HttpListener listener = new HttpListener();
            string host = "http://localhost:8081/";
            listener.Prefixes.Add(host);
            listener.Start();
            Console.WriteLine($"Listening on {listener.Prefixes.FirstOrDefault()}...");
            bool stayIn = true;
            do
            {
                HttpListenerContext context = listener.GetContext();
                using (HttpListenerResponse resp = context.Response)
                {
                    string type = await GetTypeFromRequest(context);
                    ChangeCooling(type);
                    resp.StatusCode = (int)HttpStatusCode.OK;
                    resp.StatusDescription = "Status OK";
                }
            } while (stayIn);

            listener.Stop();



            Console.ReadKey();
        }

        private static void ChangeCooling(string type)
        {
            switch (type)
            {
                case "akku":
                    aktualisMotorallapot.AkkuHutes = !aktualisMotorallapot.AkkuHutes;
                    break;
                case "motor":
                    aktualisMotorallapot.MotorHutes = !aktualisMotorallapot.MotorHutes;
                    break;
                default:
                    break;
            }
            Console.WriteLine($"Hűtés kapcsolás. Motorhűtés: {(aktualisMotorallapot.MotorHutes ? "Be" : "Ki")} Akkuhűtés: {(aktualisMotorallapot.AkkuHutes ? "Be" : "Ki")}");
        }

        private static async Task<string> GetTypeFromRequest(HttpListenerContext context)
        {
            string type = "";
            string requestBody = await new StreamReader(context.Request.InputStream).ReadToEndAsync();
            if (!string.IsNullOrEmpty(requestBody))
            {
                dynamic message = Newtonsoft.Json.JsonConvert.DeserializeObject(requestBody ?? "");
                type = message.tipus;
            }

            return type;
        }

        private static async Task TorlesKerdes()
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("Töröljem a korábbi adatokat? (I/N)");
            string key = Console.ReadKey().KeyChar.ToString();
            Console.WriteLine("");
            if (key == "i")
            {
                try
                {
                    await client.DeleteAsync(client.BaseAddress);
                    Console.WriteLine("Korábbi adatok törölve.");
                }
                catch (Exception)
                {

                    Console.WriteLine("Nincs kapcsolat a parttal.");
                    Console.WriteLine("A korábbi adatokat nem töröltem.");

                }
            }
            else
            {
                Console.WriteLine("A korábbi adatokat nem töröltem.");
            }
        }

        private static void WaitingForTheStart()
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("A hajó jelenlegi állapota:");
            AllapotKiiras();
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Nyomj meg egy gombot az indításhoz!");

            Console.ReadKey();

            Console.WriteLine("");
            Console.WriteLine("A hajó elindult.");
        }

        private static async void TimerCallBack(object? state)
        {
            if (inUse)
            {
                return;
            }

            inUse = true;

            try
            {
                AllapotValtozas();

                string json = System.Text.Json.JsonSerializer.Serialize(aktualisMotorallapot);
                //Console.WriteLine(json);
                HttpContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                await client.PostAsync(client.BaseAddress, content);
            }
            catch (Exception e)
            {
                Console.WriteLine("Hiba: Nincs kapcsolat a parttal.");

            }

            inUse = false;
        }



        private static void AllapotValtozas()
        {
            aktualisMotorallapot.AkkuHomerseklet++;
            aktualisMotorallapot.MotorHomerseklet++;
            aktualisMotorallapot.AkkuToltoaram = TulajdonsagValtozas(aktualisMotorallapot.AkkuToltoaram, minimumMotorallapot.AkkuToltoaram, maximumMotorallapot.AkkuToltoaram, maximumMotorallapotValtozas.AkkuToltoaram);
            aktualisMotorallapot.MotorAram = TulajdonsagValtozas(aktualisMotorallapot.MotorAram, minimumMotorallapot.MotorAram, maximumMotorallapot.MotorAram, maximumMotorallapotValtozas.MotorAram);
            aktualisMotorallapot.AkkuFeszultseg = TulajdonsagValtozas(aktualisMotorallapot.AkkuFeszultseg, minimumMotorallapot.AkkuFeszultseg, maximumMotorallapot.AkkuFeszultseg, maximumMotorallapotValtozas.AkkuFeszultseg);
            aktualisMotorallapot.AkkuHomerseklet = TulajdonsagValtozas(aktualisMotorallapot.AkkuHomerseklet, minimumMotorallapot.AkkuHomerseklet, maximumMotorallapot.AkkuHomerseklet, maximumMotorallapotValtozas.AkkuHomerseklet, aktualisMotorallapot.AkkuHutes);
            aktualisMotorallapot.MotorHomerseklet = TulajdonsagValtozas(aktualisMotorallapot.MotorHomerseklet, minimumMotorallapot.MotorHomerseklet, maximumMotorallapot.MotorHomerseklet, maximumMotorallapotValtozas.MotorHomerseklet, aktualisMotorallapot.MotorHutes);
            aktualisMotorallapot.RelativSebesseg = TulajdonsagValtozas(aktualisMotorallapot.RelativSebesseg, minimumMotorallapot.RelativSebesseg, maximumMotorallapot.RelativSebesseg, maximumMotorallapotValtozas.RelativSebesseg);
            aktualisMotorallapot.AbszolutSebesseg = TulajdonsagValtozas(aktualisMotorallapot.AbszolutSebesseg, minimumMotorallapot.AbszolutSebesseg, maximumMotorallapot.AbszolutSebesseg, maximumMotorallapotValtozas.AbszolutSebesseg);
            aktualisMotorallapot.Fenyerosseg = TulajdonsagValtozas(aktualisMotorallapot.Fenyerosseg, minimumMotorallapot.Fenyerosseg, maximumMotorallapot.Fenyerosseg, maximumMotorallapotValtozas.Fenyerosseg);

            AllapotKiiras();
        }

        private static void AllapotKiiras()
        {
            Console.WriteLine(DateTime.Now.ToString() + " - Levegőhőmérséklet: " + levegoHomerseklet);
            Console.WriteLine(
                            "M.áram: " + aktualisMotorallapot.MotorAram
                            + ", M.hő: " + aktualisMotorallapot.MotorHomerseklet
                            + ", A.áram: " + aktualisMotorallapot.AkkuToltoaram
                            + ", A.fesz: " + aktualisMotorallapot.AkkuFeszultseg
                            + ", A.hő: " + aktualisMotorallapot.AkkuHomerseklet
                            + ", R.seb: " + aktualisMotorallapot.RelativSebesseg
                            + ", A.seb: " + aktualisMotorallapot.AbszolutSebesseg
                            + ", Fény: " + aktualisMotorallapot.Fenyerosseg
                            + ", M.hűt: " + (aktualisMotorallapot.MotorHutes ? "Be" : "Ki")
                            + ", A.hűt: " + (aktualisMotorallapot.AkkuHutes ? "Be" : "Ki")
                        );
        }

        private static int TulajdonsagValtozas(int aktualis, int minimum, int maximum, int maxvaltozas, bool hutes = false)
        {
            int valtozas = rnd.Next(maxvaltozas * 2 + 1) - maxvaltozas;
            aktualis += valtozas;
            if (hutes)
            {
                int kulonbseg = aktualis - minimum;
                int hutesvaltozas = (int)Math.Floor(kulonbseg * 0.2);
                aktualis -= hutesvaltozas;
            }
            if (aktualis < minimum)
            {
                aktualis = minimum;
            }
            if (aktualis > maximum)
            {
                aktualis = maximum;
            }
            return aktualis;
        }
    }
}