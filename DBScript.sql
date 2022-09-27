--Ejecutar primero la creacion de base de datos
CREATE DATABASE [SistemaRH]


--Una vez creada ejecutar lo siguiente:
USE [SistemaRH]
GO

/****** Object:  Table [dbo].[user]    Script Date: 27/09/2022 01:12:31 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[user](
	[IdUser] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[User] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Status] [bit] NOT NULL,
	[Gender] [varchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[user] ADD  CONSTRAINT [DF_user_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

