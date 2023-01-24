Use SolarBoat;
--drop table Data
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Data]') AND type in (N'U'))
CREATE TABLE [dbo].[Data](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Idopont] [datetime],
	[AkkuFeszultseg] [int],
	[AkkuToltoaram] [int],
	[AkkuHomerseklet] [int],
	[MotorHomerseklet] [int],
	[MotorAram] [int],
	[AkkuHutes] [bit],
	[MotorHutes] [bit],
	[RelativSebesseg] int,
	[AbszolutSebesseg] int,
	[Fenyerosseg] int
	
CONSTRAINT [PK_Data] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 85) ON [PRIMARY]
) ON [PRIMARY]
GO

