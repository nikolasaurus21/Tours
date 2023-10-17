USE [Biblioteka]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 9/8/2023 4:37:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 9/8/2023 4:37:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 9/8/2023 4:37:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 9/8/2023 4:37:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 9/8/2023 4:37:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[Phone] [nvarchar](30) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[UserPhoto] [varbinary](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Autori]    Script Date: 9/8/2023 4:37:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Autori](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ime] [nvarchar](100) NOT NULL,
	[Prezime] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Autori] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clanovi]    Script Date: 9/8/2023 4:37:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clanovi](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ime] [nvarchar](100) NOT NULL,
	[Prezime] [nvarchar](100) NOT NULL,
	[MaticniBroj] [nvarchar](13) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[Adresa] [nvarchar](256) NOT NULL,
	[DatumRodjenja] [date] NOT NULL,
 CONSTRAINT [PK_Clanovi] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IzdanjaKnjiga]    Script Date: 9/8/2023 4:37:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IzdanjaKnjiga](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[KnjigeId] [int] NOT NULL,
	[IzdavackaKucaId] [int] NOT NULL,
	[SlikaKorica] [nvarchar](256) NULL,
	[Godina] [int] NOT NULL,
	[BrojNaStanju] [int] NOT NULL,
	[BrojIzdatih] [int] NOT NULL,
 CONSTRAINT [PK_IzdanjaKnjiga] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IzdavackeKuce]    Script Date: 9/8/2023 4:37:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IzdavackeKuce](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Naziv] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_IzdavackeKuce] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kategorije]    Script Date: 9/8/2023 4:37:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kategorije](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Naziv] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Kategorije] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Knjige]    Script Date: 9/8/2023 4:37:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Knjige](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Naslov] [nvarchar](100) NOT NULL,
	[AutorId] [int] NOT NULL,
	[GodinaOriginala] [int] NOT NULL,
	[KategorijaId] [int] NOT NULL,
	[BrojNaStanju] [int] NOT NULL,
	[BrojIzdatih] [int] NOT NULL,
 CONSTRAINT [PK_Knjige] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pozajmice]    Script Date: 9/8/2023 4:37:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pozajmice](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BibliotekarId] [nvarchar](128) NOT NULL,
	[ClanId] [int] NOT NULL,
	[PrimjerakKnjigeId] [int] NOT NULL,
	[DatumPozajmice] [date] NOT NULL,
	[DatumZakazanogVracanja] [date] NOT NULL,
	[DatumVracanja] [date] NULL,
 CONSTRAINT [PK_Pozajmice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PrimjerciKnjiga]    Script Date: 9/8/2023 4:37:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrimjerciKnjiga](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Sifra] [nvarchar](50) NOT NULL,
	[IzdanjeKnjigeId] [int] NOT NULL,
	[Zaduzen] [bit] NOT NULL,
 CONSTRAINT [PK_PrimjerciKnjiga] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IzdanjaKnjiga] ADD  CONSTRAINT [DF_IzdanjaKnjiga_BrojNaStanju]  DEFAULT ((0)) FOR [BrojNaStanju]
GO
ALTER TABLE [dbo].[IzdanjaKnjiga] ADD  CONSTRAINT [DF_IzdanjaKnjiga_BrojIzdatih]  DEFAULT ((0)) FOR [BrojIzdatih]
GO
ALTER TABLE [dbo].[Knjige] ADD  CONSTRAINT [DF_Knjige_BrojNaStanju]  DEFAULT ((0)) FOR [BrojNaStanju]
GO
ALTER TABLE [dbo].[Knjige] ADD  CONSTRAINT [DF_Knjige_BrojIzdatih]  DEFAULT ((0)) FOR [BrojIzdatih]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[IzdanjaKnjiga]  WITH CHECK ADD  CONSTRAINT [FK_IzdanjaKnjiga_IzdavackeKuce] FOREIGN KEY([IzdavackaKucaId])
REFERENCES [dbo].[IzdavackeKuce] ([Id])
GO
ALTER TABLE [dbo].[IzdanjaKnjiga] CHECK CONSTRAINT [FK_IzdanjaKnjiga_IzdavackeKuce]
GO
ALTER TABLE [dbo].[IzdanjaKnjiga]  WITH CHECK ADD  CONSTRAINT [FK_IzdanjaKnjiga_Knjige] FOREIGN KEY([KnjigeId])
REFERENCES [dbo].[Knjige] ([Id])
GO
ALTER TABLE [dbo].[IzdanjaKnjiga] CHECK CONSTRAINT [FK_IzdanjaKnjiga_Knjige]
GO
ALTER TABLE [dbo].[Knjige]  WITH CHECK ADD  CONSTRAINT [FK_Knjige_Autori] FOREIGN KEY([AutorId])
REFERENCES [dbo].[Autori] ([Id])
GO
ALTER TABLE [dbo].[Knjige] CHECK CONSTRAINT [FK_Knjige_Autori]
GO
ALTER TABLE [dbo].[Knjige]  WITH CHECK ADD  CONSTRAINT [FK_Knjige_Kategorije] FOREIGN KEY([KategorijaId])
REFERENCES [dbo].[Kategorije] ([Id])
GO
ALTER TABLE [dbo].[Knjige] CHECK CONSTRAINT [FK_Knjige_Kategorije]
GO
ALTER TABLE [dbo].[Pozajmice]  WITH CHECK ADD  CONSTRAINT [FK_Pozajmice_AspNetUsers] FOREIGN KEY([BibliotekarId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Pozajmice] CHECK CONSTRAINT [FK_Pozajmice_AspNetUsers]
GO
ALTER TABLE [dbo].[Pozajmice]  WITH CHECK ADD  CONSTRAINT [FK_Pozajmice_Clanovi] FOREIGN KEY([ClanId])
REFERENCES [dbo].[Clanovi] ([Id])
GO
ALTER TABLE [dbo].[Pozajmice] CHECK CONSTRAINT [FK_Pozajmice_Clanovi]
GO
ALTER TABLE [dbo].[Pozajmice]  WITH CHECK ADD  CONSTRAINT [FK_Pozajmice_PrimjerciKnjiga] FOREIGN KEY([PrimjerakKnjigeId])
REFERENCES [dbo].[PrimjerciKnjiga] ([Id])
GO
ALTER TABLE [dbo].[Pozajmice] CHECK CONSTRAINT [FK_Pozajmice_PrimjerciKnjiga]
GO
ALTER TABLE [dbo].[PrimjerciKnjiga]  WITH CHECK ADD  CONSTRAINT [FK_PrimjerciKnjiga_IzdanjaKnjiga] FOREIGN KEY([IzdanjeKnjigeId])
REFERENCES [dbo].[IzdanjaKnjiga] ([Id])
GO
ALTER TABLE [dbo].[PrimjerciKnjiga] CHECK CONSTRAINT [FK_PrimjerciKnjiga_IzdanjaKnjiga]
GO
