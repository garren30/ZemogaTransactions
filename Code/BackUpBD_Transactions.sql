USE [master]
GO
/****** Object:  Database [TransactionsBD]    Script Date: 01/11/2017 17:03:08 ******/
CREATE DATABASE [TransactionsBD] ON  PRIMARY 
( NAME = N'TransactionsBD', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\TransactionsBD.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TransactionsBD_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\TransactionsBD_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TransactionsBD].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TransactionsBD] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TransactionsBD] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TransactionsBD] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TransactionsBD] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TransactionsBD] SET ARITHABORT OFF 
GO
ALTER DATABASE [TransactionsBD] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TransactionsBD] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TransactionsBD] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TransactionsBD] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TransactionsBD] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TransactionsBD] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TransactionsBD] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TransactionsBD] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TransactionsBD] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TransactionsBD] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TransactionsBD] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TransactionsBD] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TransactionsBD] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TransactionsBD] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TransactionsBD] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TransactionsBD] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TransactionsBD] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TransactionsBD] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TransactionsBD] SET  MULTI_USER 
GO
ALTER DATABASE [TransactionsBD] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TransactionsBD] SET DB_CHAINING OFF 
GO
USE [TransactionsBD]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 01/11/2017 17:03:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Clients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientCode] [varchar](50) NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 01/11/2017 17:03:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[rol] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 01/11/2017 17:03:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[transactiontype_id] [int] NOT NULL,
	[amount] [float] NOT NULL,
	[clientorg_Id] [int] NOT NULL,
	[oldbalanceorg] [float] NULL,
	[newbalanceorg] [float] NULL,
	[clientdest_Id] [int] NOT NULL,
	[oldbalancedest] [float] NULL,
	[isfraud] [int] NULL,
	[usercreation_id] [int] NOT NULL,
	[usermarkfraud_id] [int] NULL,
	[frauddate] [datetime] NULL,
	[newbalancedest] [float] NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Transactiontypes]    Script Date: 01/11/2017 17:03:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Transactiontypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[transactiontype] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Transactiontypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 01/11/2017 17:03:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password] [varchar](100) NOT NULL,
	[rol_id] [int] NOT NULL,
	[name] [varchar](200) NOT NULL,
	[created_at] [datetime] NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [rol]) VALUES (1, N'Administrator')
INSERT [dbo].[Roles] ([Id], [rol]) VALUES (2, N'Manager')
INSERT [dbo].[Roles] ([Id], [rol]) VALUES (3, N'Superintendent')
INSERT [dbo].[Roles] ([Id], [rol]) VALUES (4, N'Assistant')
SET IDENTITY_INSERT [dbo].[Roles] OFF
SET IDENTITY_INSERT [dbo].[Transactiontypes] ON 

INSERT [dbo].[Transactiontypes] ([Id], [transactiontype]) VALUES (1, N'payment')
INSERT [dbo].[Transactiontypes] ([Id], [transactiontype]) VALUES (2, N'Transfer')
INSERT [dbo].[Transactiontypes] ([Id], [transactiontype]) VALUES (3, N'debit')
INSERT [dbo].[Transactiontypes] ([Id], [transactiontype]) VALUES (4, N'cash_out')
SET IDENTITY_INSERT [dbo].[Transactiontypes] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [username], [password], [rol_id], [name], [created_at]) VALUES (1, N'Administrator', N'Fzr3XPlLZ81S2mb0LIhulrNE7MQ=', 1, N'Administrator', CAST(N'2017-10-31 00:00:00.000' AS DateTime))
INSERT [dbo].[Users] ([Id], [username], [password], [rol_id], [name], [created_at]) VALUES (3, N'Superintendent', N'7pumWxr0MfFFvZhFY5HQUwH03Tg=', 3, N'Superintendent', CAST(N'2017-10-31 00:00:00.000' AS DateTime))
INSERT [dbo].[Users] ([Id], [username], [password], [rol_id], [name], [created_at]) VALUES (5, N'Manager', N'eDG3cVqglwO+ptq2bZEOYOtonD8=', 2, N'Manager', CAST(N'2017-11-01 13:30:46.730' AS DateTime))
INSERT [dbo].[Users] ([Id], [username], [password], [rol_id], [name], [created_at]) VALUES (6, N'Assistant', N'S/jryIfVKjMFEt5HgdqwGnbSNQc=', 4, N'Assistant', CAST(N'2017-11-01 13:42:28.757' AS DateTime))
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Clients_dest] FOREIGN KEY([clientdest_Id])
REFERENCES [dbo].[Clients] ([Id])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Clients_dest]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Clients_org] FOREIGN KEY([clientorg_Id])
REFERENCES [dbo].[Clients] ([Id])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Clients_org]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Transactiontypes] FOREIGN KEY([transactiontype_id])
REFERENCES [dbo].[Transactiontypes] ([Id])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Transactiontypes]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_User_Creation] FOREIGN KEY([usercreation_id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_User_Creation]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Users_Fraud] FOREIGN KEY([usermarkfraud_id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Users_Fraud]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([rol_id])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
/****** Object:  StoredProcedure [dbo].[GetTransactionData]    Script Date: 01/11/2017 17:03:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetTransactionData]
@clientorgdcode varchar(50),
@clientdestcode varchar(50),
@amount float,
@transactiontype_id int,
@usercreation_id int,
@Identity int OUT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;
    -- Insert statements for procedure here
	declare @idClientorg int;
	declare @idClientdest int;
	declare @oldbalanceorg float;
	declare @newbalanceorg float;
	declare @newbalancedest float;
	declare @oldbalancedest float;
	declare @idClientorgtemp int;
	declare @newbalanceorgtemp float;
	declare @newbalancedesttemp float;
	set @Identity=0;
	-----Validate if the client org exists and set id to the variable
	if(@clientorgdcode != 'null')
		begin
			if exists (select * from dbo.Clients where ClientCode=@clientorgdcode)
				begin
					PRINT '1'
					set @idClientorg=(select top(1) Id from dbo.Clients where ClientCode=@clientorgdcode)
				end
			else
				begin
					PRINT '2'
					--if the client org doesn't exists add to de bd and set id to the variable
					insert into dbo.Clients (ClientCode) values (@clientorgdcode)
					set @idClientorg=(select top(1) Id from dbo.Clients where ClientCode=@clientorgdcode)
				end
		end
	-----Validate if the client dest exists and set id to the variable
	if(@clientdestcode!= 'null')
		begin
			if exists (select * from dbo.Clients where ClientCode=@clientdestcode)
				set @idClientdest=(select top(1) Id from dbo.Clients where ClientCode=@clientdestcode)
			else
				--if the client dest doesn't exists add to de bd and set id to the variable
				insert into dbo.Clients (ClientCode) values (@clientdestcode)
				set @idClientdest=(select top(1) Id from dbo.Clients where ClientCode=@clientdestcode)
		end
		
	if exists (select * from dbo.Transactions where clientorg_Id=@idClientorg or clientdest_Id=@idClientorg)
		begin
			select top(1) @newbalanceorgtemp=newbalanceorg, @newbalancedesttemp=newbalancedest, @idClientorgtemp=clientorg_Id from dbo.Transactions where clientorg_Id=@idClientorg or clientdest_Id=@idClientorg order by TransactionDate desc
			if(@idClientorgtemp=@idClientorg)
				set @oldbalanceorg=@newbalanceorgtemp;
			else
				set @oldbalanceorg=@newbalancedesttemp;
		end
	else
		set @oldbalanceorg=0;




	if exists (select * from dbo.Transactions where clientorg_Id=@idClientdest or clientdest_Id=@idClientdest)
		begin
			select top(1) @newbalanceorgtemp=newbalanceorg, @newbalancedesttemp=newbalancedest, @idClientorgtemp=clientorg_Id from dbo.Transactions where clientorg_Id=@idClientdest or clientdest_Id=@idClientdest order by TransactionDate desc
			if(@idClientorgtemp=@idClientdest)
				set @oldbalancedest=@newbalanceorgtemp;
			else
				set @oldbalancedest=@newbalancedesttemp;
		end
	else
		set @oldbalancedest=0;
		
	
	set @newbalanceorg = @oldbalanceorg-@amount;
	if(@newbalanceorg<0)
		set @newbalanceorg=0;

	set @newbalancedest=
		case 
			when @transactiontype_id =1 then @oldbalancedest
			when @transactiontype_id =2 then @oldbalancedest+@amount
			when @transactiontype_id =3 then @oldbalancedest+@amount
			when @transactiontype_id =4 then @oldbalancedest+@amount
		end;
	if(@newbalancedest<0)
		set @newbalancedest=0;
	if(@idClientdest!=0 and @idClientorg!=0)
		begin
			insert into Transactions (TransactionDate, transactiontype_id, amount, clientorg_Id, oldbalanceorg, newbalanceorg, clientdest_Id, oldbalancedest, isfraud, usercreation_id, newbalancedest) 
			values (GETDATE(), @transactiontype_id, @amount, @idClientorg, @oldbalanceorg, @newbalanceorg, @idClientdest, @oldbalancedest, 0, @usercreation_id, @newbalancedest);
			SELECT @Identity = SCOPE_IDENTITY();
			--RETURN
		end
	
END

GO
USE [master]
GO
ALTER DATABASE [TransactionsBD] SET  READ_WRITE 
GO
