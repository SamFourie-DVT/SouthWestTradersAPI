USE [SouthWestTraders]
GO

/****** Object:  Table [dbo].[Order]    Script Date: 2022/04/26 08:42:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Order](
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[CreatedDateUTC] [date] NOT NULL,
	[Quantity] [int] NOT NULL,
	[OrderStateId] [int] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_OrderState] FOREIGN KEY([OrderStateId])
REFERENCES [dbo].[OrderState] ([OrderStateId])
GO

ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_OrderState]
GO

ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO

ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Product]
GO


/* 
ORDER STATE
*/

USE [SouthWestTraders]
GO

/****** Object:  Table [dbo].[OrderState]    Script Date: 2022/04/26 08:42:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OrderState](
	[OrderStateId] [int] NOT NULL,
	[State] [varchar](50) NOT NULL,
 CONSTRAINT [PK_OrderState] PRIMARY KEY CLUSTERED 
(
	[OrderStateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/*
PRODUCT TABLE
*/
USE [SouthWestTraders]
GO

/****** Object:  Table [dbo].[Product]    Script Date: 2022/04/26 08:42:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Product](
	[ProductId] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](150) NOT NULL,
	[Price] [money] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/*
STOCK TABLE
*/
USE [SouthWestTraders]
GO

/****** Object:  Table [dbo].[Stock]    Script Date: 2022/04/26 08:42:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Stock](
	[StockId] [int] NOT NULL,
	[ProductId] [int] NULL,
	[AvailableStock] [int] NULL,
 CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED 
(
	[StockId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO

ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_Product]
GO