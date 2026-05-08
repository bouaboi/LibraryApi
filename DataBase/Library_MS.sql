USE [master]
GO
/****** Object:  Database [Library_MS]    Script Date: 5/8/2026 2:47:00 PM ******/
CREATE DATABASE [Library_MS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Library_MS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Library_MS.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Library_MS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Library_MS_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Library_MS] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Library_MS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Library_MS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Library_MS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Library_MS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Library_MS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Library_MS] SET ARITHABORT OFF 
GO
ALTER DATABASE [Library_MS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Library_MS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Library_MS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Library_MS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Library_MS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Library_MS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Library_MS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Library_MS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Library_MS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Library_MS] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Library_MS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Library_MS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Library_MS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Library_MS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Library_MS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Library_MS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Library_MS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Library_MS] SET RECOVERY FULL 
GO
ALTER DATABASE [Library_MS] SET  MULTI_USER 
GO
ALTER DATABASE [Library_MS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Library_MS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Library_MS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Library_MS] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Library_MS] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Library_MS] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Library_MS', N'ON'
GO
ALTER DATABASE [Library_MS] SET QUERY_STORE = ON
GO
ALTER DATABASE [Library_MS] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Library_MS]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[BookID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[ISBN] [nvarchar](25) NOT NULL,
	[Genre] [nvarchar](50) NULL,
	[Author] [nvarchar](100) NOT NULL,
	[TotalCopies] [int] NULL,
	[AvailableCopies] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[BookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Borrows]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Borrows](
	[BorrowID] [int] IDENTITY(1,1) NOT NULL,
	[MemberID] [int] NULL,
	[BookID] [int] NULL,
	[BorrowDate] [date] NULL,
	[DueDate] [date] NULL,
	[ReturnDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[BorrowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fines]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fines](
	[FineID] [int] IDENTITY(1,1) NOT NULL,
	[BorrowID] [int] NULL,
	[Amount] [decimal](10, 2) NULL,
	[PaidAmount] [decimal](10, 2) NULL,
	[CreatedDate] [date] NULL,
	[IsSettled] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[FineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Members]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Members](
	[MemberID] [int] IDENTITY(1,1) NOT NULL,
	[NationalID] [nvarchar](20) NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[DateOfBirth] [date] NULL,
	[Address] [nvarchar](250) NULL,
	[Phone] [nvarchar](15) NULL,
	[DateOfJoin] [date] NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[MemberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settings](
	[SettingID] [int] IDENTITY(1,1) NOT NULL,
	[MaxBorrowLimit] [int] NULL,
	[BorrowDurationDays] [int] NULL,
	[DailyFineRate] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[SettingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Books] ON 
GO
INSERT [dbo].[Books] ([BookID], [Title], [ISBN], [Genre], [Author], [TotalCopies], [AvailableCopies]) VALUES (3, N'White Nights', N'9781548269357', N'Classics', N'Fyodor Dostoevsky', 4, 4)
GO
INSERT [dbo].[Books] ([BookID], [Title], [ISBN], [Genre], [Author], [TotalCopies], [AvailableCopies]) VALUES (4, N'Animal Farm', N'9780451526342', N'Classics', N'George Orwell', 4, 4)
GO
INSERT [dbo].[Books] ([BookID], [Title], [ISBN], [Genre], [Author], [TotalCopies], [AvailableCopies]) VALUES (5, N'The Picture of Dorian Gray', N'9780141439570', N'Classics', N'Oscar Wilde', 4, 4)
GO
SET IDENTITY_INSERT [dbo].[Books] OFF
GO
SET IDENTITY_INSERT [dbo].[Borrows] ON 
GO
INSERT [dbo].[Borrows] ([BorrowID], [MemberID], [BookID], [BorrowDate], [DueDate], [ReturnDate]) VALUES (1, 1, 3, CAST(N'2026-05-06' AS Date), CAST(N'2026-01-01' AS Date), CAST(N'2026-05-06' AS Date))
GO
INSERT [dbo].[Borrows] ([BorrowID], [MemberID], [BookID], [BorrowDate], [DueDate], [ReturnDate]) VALUES (2, 3, 3, CAST(N'2026-05-07' AS Date), CAST(N'2026-05-21' AS Date), CAST(N'2026-05-07' AS Date))
GO
SET IDENTITY_INSERT [dbo].[Borrows] OFF
GO
SET IDENTITY_INSERT [dbo].[Fines] ON 
GO
INSERT [dbo].[Fines] ([FineID], [BorrowID], [Amount], [PaidAmount], [CreatedDate], [IsSettled]) VALUES (1, 1, CAST(62.50 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(N'2026-05-06' AS Date), 1)
GO
SET IDENTITY_INSERT [dbo].[Fines] OFF
GO
SET IDENTITY_INSERT [dbo].[Members] ON 
GO
INSERT [dbo].[Members] ([MemberID], [NationalID], [FirstName], [LastName], [DateOfBirth], [Address], [Phone], [DateOfJoin], [IsActive]) VALUES (1, N'1478966', N'Ali', N'Hazem', CAST(N'1996-11-22' AS Date), N'BB-MO Rue Indie 09', N'0122478665', CAST(N'2026-05-02' AS Date), 1)
GO
INSERT [dbo].[Members] ([MemberID], [NationalID], [FirstName], [LastName], [DateOfBirth], [Address], [Phone], [DateOfJoin], [IsActive]) VALUES (2, N'9876543', N'Sara', N'Ahmed', CAST(N'1995-08-20' AS Date), N'456 Cairo Street', N'0559876543', CAST(N'2026-05-02' AS Date), 1)
GO
INSERT [dbo].[Members] ([MemberID], [NationalID], [FirstName], [LastName], [DateOfBirth], [Address], [Phone], [DateOfJoin], [IsActive]) VALUES (3, N'2456879', N'Djamil', N'Hamada', CAST(N'1999-07-13' AS Date), N'47 Street Of Tunisia', N'0789610424', CAST(N'2026-05-03' AS Date), 1)
GO
INSERT [dbo].[Members] ([MemberID], [NationalID], [FirstName], [LastName], [DateOfBirth], [Address], [Phone], [DateOfJoin], [IsActive]) VALUES (4, N'2278436', N'Fati', N'Samer', CAST(N'1980-06-13' AS Date), N'B1 Oman City', N'0322998675', CAST(N'2026-05-08' AS Date), 1)
GO
SET IDENTITY_INSERT [dbo].[Members] OFF
GO
SET IDENTITY_INSERT [dbo].[Settings] ON 
GO
INSERT [dbo].[Settings] ([SettingID], [MaxBorrowLimit], [BorrowDurationDays], [DailyFineRate]) VALUES (1, 3, 20, CAST(0.50 AS Decimal(10, 2)))
GO
SET IDENTITY_INSERT [dbo].[Settings] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Books__447D36EA25E2D0D5]    Script Date: 5/8/2026 2:47:00 PM ******/
ALTER TABLE [dbo].[Books] ADD UNIQUE NONCLUSTERED 
(
	[ISBN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ_Fines_BorrowID]    Script Date: 5/8/2026 2:47:00 PM ******/
ALTER TABLE [dbo].[Fines] ADD  CONSTRAINT [UQ_Fines_BorrowID] UNIQUE NONCLUSTERED 
(
	[BorrowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Members__E9AA321A51DDF093]    Script Date: 5/8/2026 2:47:00 PM ******/
ALTER TABLE [dbo].[Members] ADD UNIQUE NONCLUSTERED 
(
	[NationalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Borrows] ADD  DEFAULT (getdate()) FOR [BorrowDate]
GO
ALTER TABLE [dbo].[Fines] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Borrows]  WITH CHECK ADD FOREIGN KEY([BookID])
REFERENCES [dbo].[Books] ([BookID])
GO
ALTER TABLE [dbo].[Borrows]  WITH CHECK ADD FOREIGN KEY([MemberID])
REFERENCES [dbo].[Members] ([MemberID])
GO
ALTER TABLE [dbo].[Fines]  WITH CHECK ADD FOREIGN KEY([BorrowID])
REFERENCES [dbo].[Borrows] ([BorrowID])
GO
/****** Object:  StoredProcedure [dbo].[SP_AddBook]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_AddBook]

    @Title NVARCHAR(250),
    @ISBN NVARCHAR(25),
    @Genre NVARCHAR(150),
    @Author NVARCHAR(100),
    @TotalCopies INT,
    @NewBookID INT OUTPUT

    AS
    BEGIN

         BEGIN TRANSACTION;
         BEGIN TRY

    INSERT INTO Books(Title, ISBN, Genre, Author, TotalCopies, AvailableCopies)
    VALUES (@Title, @ISBN, @Genre, @Author, @TotalCopies, @TotalCopies)

    SET @NewBookID = SCOPE_IDENTITY();

         COMMIT TRANSACTION;
         END TRY
      BEGIN CATCH
         ROLLBACK TRANSACTION;
         THROW;
         END CATCH

     END







GO
/****** Object:  StoredProcedure [dbo].[SP_AddBorrow]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_AddBorrow]


          @MemberID INT,
          @BookID INT,
          @NewBorrowID INT OUTPUT

     AS
     BEGIN
          BEGIN TRANSACTION;
          BEGIN TRY


          DECLARE @MaxBorrowLimit INT
          DECLARE @BorrowDurationDays INT

          SELECT @MaxBorrowLimit = MaxBorrowLimit,
                 @BorrowDurationDays = BorrowDurationDays

           FROM Settings WHERE SettingID = 1
                 
           
           DECLARE @CurrentBorrow INT
           SELECT @CurrentBorrow = COUNT(*)
           FROM Borrows WHERE MemberID = @MemberID AND ReturnDate IS NULL 


IF @CurrentBorrow >= @MaxBorrowLimit
BEGIN
    SET @NewBorrowID = -1
    ROLLBACK TRANSACTION;
    RETURN
END

           DECLARE @AvailableCopies INT
           SELECT @AvailableCopies = AvailableCopies FROM Books WHERE BookID = @BookID
         
IF @AvailableCopies < 1
BEGIN
    SET @NewBorrowID = -2
    ROLLBACK TRANSACTION;
    RETURN
END

DECLARE @UnpaidFines INT
SELECT @UnpaidFines = COUNT(*) 
FROM Fines AS F
INNER JOIN Borrows AS B ON F.BorrowID = B.BorrowID
WHERE B.MemberID = @MemberID AND F.IsSettled = 0

IF @UnpaidFines > 0
BEGIN
    SET @NewBorrowID = -3
    ROLLBACK TRANSACTION;
    RETURN
END

           INSERT INTO Borrows (MemberID, BookID, BorrowDate, DueDate)
           VALUES (@MemberID, @BookID, GETDATE(), DATEADD(DAY, @BorrowDurationDays, GETDATE()))

           SET @NewBorrowID = SCOPE_IDENTITY()

           UPDATE Books SET AvailableCopies = @AvailableCopies - 1 WHERE BookID = @BookID

           
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_AddMember]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_AddMember]
    @NationalID NVARCHAR(20),
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @DateOfBirth Date,
    @Address NVARCHAR(250),
    @Phone NVARCHAR(15),
    @NewMemberID INT OUTPUT
AS
BEGIN
    
    BEGIN TRANSACTION;

    BEGIN TRY 

    INSERT INTO Members(NationalID, FirstName, LastName, DateOfBirth, Address, Phone, DateOfJoin, IsActive)
    VALUES (@NationalID, @FirstName, @LastName, @DateOfBirth, @Address, @Phone, GETDATE(), 1);


    SET @NewMemberID = SCOPE_IDENTITY();

   COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[SP_DeactivateMember]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DeactivateMember]
    @MemberID INT
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        UPDATE Members
        SET IsActive = 0
        WHERE MemberID = @MemberID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteBook]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_DeleteBook]

   @bookID INT
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        DELETE FROM Books
        
        WHERE BookID = @bookID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetActiveBorrows]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_GetActiveBorrows] 


AS 
BEGIN 

       SELECT B.BorrowID, 
       B.MemberID, M.FirstName + ' ' + M.LastName AS MemberName,
       B.BookID, BK.Title AS BookTitle,
       B.BorrowDate, B.DueDate, B.ReturnDate

       FROM Borrows AS B
       INNER JOIN Members AS M ON B.MemberID = M.MemberID
       INNER JOIN Books AS BK ON B.BookID = BK.BookID
       WHERE B.ReturnDate IS NULL
     END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllBooks]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetAllBooks]

AS 
BEGIN 

     SELECT B.BookID, B.Title, B.ISBN, B.Genre, B.Author, B.TotalCopies, B.AvailableCopies
     FROM Books AS B

     END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllBorrows]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[SP_GetAllBorrows]



AS 
BEGIN 
       SELECT B.BorrowID, 
       B.MemberID, M.FirstName + ' ' + M.LastName AS MemberName,
       B.BookID, BK.Title AS BookTitle,
       B.BorrowDate, B.DueDate, B.ReturnDate

       FROM Borrows AS B
       INNER JOIN Members AS M ON B.MemberID = M.MemberID
       INNER JOIN Books AS BK ON B.BookID = BK.BookID

     END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllFines]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetAllFines]
AS
BEGIN
    SELECT F.FineID, F.BorrowID, 
           M.FirstName + ' ' + M.LastName AS MemberName,
           BK.Title AS BookTitle,
           F.Amount, F.PaidAmount, F.CreatedDate, F.IsSettled
    FROM Fines AS F
    INNER JOIN Borrows AS B ON F.BorrowID = B.BorrowID
    INNER JOIN Members AS M ON B.MemberID = M.MemberID
    INNER JOIN Books AS BK ON B.BookID = BK.BookID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllMembers]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_GetAllMembers]
AS
BEGIN

    SELECT M.MemberID, M.NationalID, M.FirstName, M.LastName, M.DateOfBirth, M.Address, M.Phone, M.DateOfJoin, M.IsActive
    FROM Members AS M
 
   
   END


GO
/****** Object:  StoredProcedure [dbo].[SP_GetBookByID]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_GetBookByID]

    @BookID INT

    AS
    BEGIN

    SELECT B.BookID, B.Title, B.ISBN, B.Genre, B.Author, B.TotalCopies, B.AvailableCopies
    FROM Books AS B

    WHERE B.BookID = @BookID;

    END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetBorrowByID]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetBorrowByID]

    @BorrowID INT

    AS
    BEGIN

SELECT B.BorrowID, 
       B.MemberID, M.FirstName + ' ' + M.LastName AS MemberName,
       B.BookID, BK.Title AS BookTitle,
       B.BorrowDate, B.DueDate, B.ReturnDate

       FROM Borrows AS B
       INNER JOIN Members AS M ON B.MemberID = M.MemberID
       INNER JOIN Books AS BK ON B.BookID = BK.BookID

    END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetBorrowsByMemberID]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[SP_GetBorrowsByMemberID]

    @MemberID INT

AS 
BEGIN 

       SELECT B.BorrowID, 
       B.MemberID, M.FirstName + ' ' + M.LastName AS MemberName,
       B.BookID, BK.Title AS BookTitle,
       B.BorrowDate, B.DueDate, B.ReturnDate

       FROM Borrows AS B
       INNER JOIN Members AS M ON B.MemberID = M.MemberID
       INNER JOIN Books AS BK ON B.BookID = BK.BookID
       where M.MemberID = @MemberID
     END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetFinesByMemberID]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_GetFinesByMemberID] 

@MemberID INT

AS
BEGIN
    SELECT F.FineID, F.BorrowID, 
           M.FirstName + ' ' + M.LastName AS MemberName,
           BK.Title AS BookTitle,
           F.Amount, F.PaidAmount, F.CreatedDate, F.IsSettled
    FROM Fines AS F
    INNER JOIN Borrows AS B ON F.BorrowID = B.BorrowID
    INNER JOIN Members AS M ON B.MemberID = M.MemberID
    INNER JOIN Books AS BK ON B.BookID = BK.BookID
    WHERE M.MemberID = @MemberID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetMemberByID]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_GetMemberByID]
    @MemberID INT
AS
BEGIN
    SELECT M.MemberID, M.NationalID, M.FirstName, M.LastName, 
           M.DateOfBirth, M.Address, M.Phone, M.DateOfJoin, M.IsActive
    FROM Members AS M
    WHERE M.MemberID = @MemberID;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetSettings]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_GetSettings]
AS 
BEGIN
    SELECT SettingID, MaxBorrowLimit, BorrowDurationDays, DailyFineRate
    FROM Settings
    WHERE SettingID = 1
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ReactivateMember]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_ReactivateMember]
    @MemberID INT
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        UPDATE Members
        SET IsActive = 1
        WHERE MemberID = @MemberID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ReturnBook]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[SP_ReturnBook] 
    @BorrowID INT
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        DECLARE @DueDate DATE
        DECLARE @BookID INT
        SELECT @DueDate = DueDate, @BookID = BookID
        FROM Borrows WHERE BorrowID = @BorrowID

        UPDATE Borrows 
        SET ReturnDate = GETDATE() 
        WHERE BorrowID = @BorrowID

        IF GETDATE() > @DueDate
        BEGIN
            DECLARE @DaysLate INT
            DECLARE @DailyFineRate DECIMAL(10,2)
            DECLARE @FineAmount DECIMAL(10,2)
            SELECT @DailyFineRate = DailyFineRate FROM Settings WHERE SettingID = 1
            SET @DaysLate = DATEDIFF(DAY, @DueDate, GETDATE())
            SET @FineAmount = @DaysLate * @DailyFineRate
            INSERT INTO Fines(BorrowID, Amount, PaidAmount, CreatedDate, IsSettled)
            VALUES (@BorrowID, @FineAmount, 0, GETDATE(), 0)
        END

        UPDATE Books 
        SET AvailableCopies = AvailableCopies + 1 
        WHERE BookID = @BookID

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SearchBook]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[SP_SearchBook]
    @Query NVARCHAR(250)
AS
BEGIN
    SELECT * FROM Books
    WHERE Title LIKE '%' + @Query + '%'
    OR Author LIKE '%' + @Query + '%'
    OR ISBN LIKE '%' + @Query + '%'
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SearchMember]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_SearchMember]
    @Query NVARCHAR(100)
AS
BEGIN
    SELECT * FROM Members
    WHERE FirstName LIKE '%' + @Query + '%'
    OR LastName LIKE '%' + @Query + '%'
    OR NationalID LIKE '%' + @Query + '%'
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SettleFine]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_SettleFine] 
    @FineID INT,
    @PaidAmount DECIMAL(10,2)
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        UPDATE Fines
        SET IsSettled = 1,
            PaidAmount = @PaidAmount
        WHERE FineID = @FineID
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateBook]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_UpdateBook]

     @BookID INT,
     @Title NVARCHAR(255),
     @ISBN NVARCHAR(25),
     @Genre NVARCHAR(50),
     @Author NVARCHAR(100),
     @TotalCopies INT

     AS
     BEGIN

     BEGIN TRANSACTION;
     BEGIN TRY

     UPDATE Books
     SET Title = @Title,
         ISBN = @ISBN,
         Genre = @Genre,
         Author = @Author,
         TotalCopies = @TotalCopies
         WHERE BookID = @BookID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END


GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateMember]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UpdateMember]
    @MemberID    INT,
    @NationalID  NVARCHAR(20),
    @FirstName   NVARCHAR(50),
    @LastName    NVARCHAR(50),
    @DateOfBirth DATE,
    @Address     NVARCHAR(250),
    @Phone       NVARCHAR(15)
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        UPDATE Members
        SET NationalID  = @NationalID,
            FirstName   = @FirstName,
            LastName    = @LastName,
            DateOfBirth = @DateOfBirth,
            Address     = @Address,
            Phone       = @Phone
        WHERE MemberID = @MemberID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateSettings]    Script Date: 5/8/2026 2:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UpdateSettings]
    @MaxBorrowLimit INT,
    @BorrowDurationDays INT,
    @DailyFineRate DECIMAL(10,2)
AS 
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        UPDATE Settings
        SET MaxBorrowLimit = @MaxBorrowLimit,
            BorrowDurationDays = @BorrowDurationDays,
            DailyFineRate = @DailyFineRate
        WHERE SettingID = 1
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO
USE [master]
GO
ALTER DATABASE [Library_MS] SET  READ_WRITE 
GO
