USE [master]
GO
/****** Object:  Database [Project 1]    Script Date: 28.04.2025 19:06:38 ******/
CREATE DATABASE [Project 1]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Project 1', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Project 1.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Project 1_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Project 1_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Project 1] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Project 1].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Project 1] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Project 1] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Project 1] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Project 1] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Project 1] SET ARITHABORT OFF 
GO
ALTER DATABASE [Project 1] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Project 1] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Project 1] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Project 1] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Project 1] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Project 1] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Project 1] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Project 1] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Project 1] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Project 1] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Project 1] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Project 1] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Project 1] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Project 1] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Project 1] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Project 1] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Project 1] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Project 1] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Project 1] SET  MULTI_USER 
GO
ALTER DATABASE [Project 1] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Project 1] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Project 1] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Project 1] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Project 1] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Project 1] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Project 1] SET QUERY_STORE = ON
GO
ALTER DATABASE [Project 1] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Project 1]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 28.04.2025 19:06:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Position] [nvarchar](255) NOT NULL,
	[Salary] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 28.04.2025 19:06:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Users_Username] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_UserRole]  DEFAULT ('User') FOR [Role]
GO
/****** Object:  StoredProcedure [dbo].[sp_RegisterUser_Plain]    Script Date: 28.04.2025 19:06:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* 2. Register procedure */
CREATE   PROCEDURE [dbo].[sp_RegisterUser_Plain]
    @UserName NVARCHAR(50),
    @Password NVARCHAR(128)
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM dbo.Users WHERE UserName = @UserName)
    BEGIN
        RAISERROR (N'User name already taken.', 16, 1);
        RETURN;
    END

    INSERT dbo.Users (UserName, Password) VALUES (@UserName, @Password);
END
GO
USE [master]
GO
ALTER DATABASE [Project 1] SET  READ_WRITE 
GO
