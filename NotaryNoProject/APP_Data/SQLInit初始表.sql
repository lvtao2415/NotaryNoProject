
 
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[SystemSet]'))
Begin
  CREATE TABLE [SystemSet](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PrinterDrive] [nvarchar](250) NULL,
	[PrinterRemark] [nvarchar](250) NULL,
	[PrinterSign] [nvarchar](250) NULL,
	[PrinterWidth]  Decimal(18,2) default(0),
	[PrinterHeight]  Decimal(18,2) default(0),
	[PrinterSpeed]  Decimal(18,2) default(0),
	[PrinterConcent]  Decimal(18,2) default(0),
	[DatabaseServer] [nvarchar](250) NULL,
	[DatabaseName] [nvarchar](250) NULL,
	[DatabaseUser] [nvarchar](250) NULL,
	[DatabasePwd] [nvarchar](250) NULL
	)
End

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[CustomerInfos]'))
Begin
  CREATE TABLE [CustomerInfos](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectName] [nvarchar](250) NULL,
	[PurchaseManager] [nvarchar](250) NULL,
	[CustomerFirstName] [nvarchar](250) NULL,
	[CustomerFirstSex] [nvarchar](250) NULL,
	[CustomerFirstCard]  [nvarchar](250) NULL,
	[CustomerFirstPhone]  [nvarchar](250) NULL,
	[CustomerSecondName] [nvarchar](250) NULL,
	[CustomerSecondSex] [nvarchar](250) NULL,
	[CustomerSecondCard]  [nvarchar](250) NULL,
	[CustomerSecondPhone]  [nvarchar](250) NULL,
	[CustomerAddress]  [nvarchar](500) NULL,
	[NotaryNo] [nvarchar](250) NULL,
	[SignNo]  [nvarchar](250) NULL,
	[SignStaus]  Int default(0),
	[SignDate] [Datetime] NULL,
	[CustomerStaus]  Int default(1),
	[CreateDate] [Datetime] NULL,
	[DelStaus] Int default(0)
	)
End

IF NOT EXISTS (SELECT 1 FROM sysobjects a left join sysColumns b on a.id=b.id WHERE a.name='CustomerInfos' and b.name='PurchaseManager')
BEGIN
	ALTER TABLE [CustomerInfos] ADD [PurchaseManager] [nvarchar](250) NULL	--������ҵ����
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[CustomerInfo]'))
Begin
  CREATE TABLE [CustomerInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] BigInt Not NULL,
	[CustomerName] [nvarchar](250) NULL,
	[CustomerSex]  Int default(1),
	[CustomerCard]  [nvarchar](250) NULL,
	[CustomerPhone]  [nvarchar](250) NULL,
	[CustomerStaus]  Int default(1),
	[CustomersID] BigInt Not NULL,
	[CreateDate] [Datetime] NULL,
	[DelStaus] Int default(0)
	)
End


IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[ProjectInfo]'))
Begin
  CREATE TABLE [ProjectInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] BigInt Not NULL,
	[ProjectName] [nvarchar](250) NULL,
	[ProjectShort]  [nvarchar](250) NULL,
	[SellerID]  BigInt Not NULL,
	[SellerName]  [nvarchar](250) NULL,
	[SellerShort] [nvarchar](250) NULL,
	[MinNo] [nvarchar](250) NULL,
	[MaxNo] [nvarchar](250) NULL,
	[StartDate] [Datetime] NULL,
	[EndDate]  [Datetime] NULL,
	[ProjectStaus]  Int default(0),
	[CreateDate]  [Datetime] NULL,
	[DelStaus] Int default(0)
	)
End

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[ProjectLable]'))
Begin
  CREATE TABLE [ProjectLable](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] BigInt Not NULL,
	[LableInfo] [nvarchar](500) NULL,
	[OrderID]  Int NULL,
	[IsBottom] Int default(0),
	[DelStaus] Int default(0)
	)
End

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[NotaryNoInfo]'))
Begin
  CREATE TABLE [NotaryNoInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] BigInt Not NULL,
	[CustomersID] BigInt Not NULL,
	[NotaryNo] [nvarchar](250) NULL,
	[SignNo]  [nvarchar](250) NULL,
	[NotaryStaus]  Int default(1),
	[CreateDate]  [Datetime] NULL,
	[DelStaus] Int default(0)
	)
End

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[�ͻ�������Ϣ��]'))
Begin
  CREATE TABLE [�ͻ�������Ϣ��](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[�ͻ�����] [nvarchar](250) NULL,
	[�ͻ��Ա�] [nvarchar](250) NULL,
	[���֤����] [nvarchar](250) NULL,
	[�ֻ�����]  [nvarchar](250) NULL,
	[�ͻ���ϵ���]  [nvarchar](250) NULL,
	[��������]  [nvarchar](250) NULL,
	[����״̬] Int default(0),
	[����ʱ��] [Datetime] NULL
	)
End

IF NOT EXISTS (SELECT 1 FROM [SystemSet])
Begin
  insert into [SystemSet]([PrinterDrive],[PrinterRemark],[PrinterWidth],[PrinterHeight],[PrinterSpeed],[PrinterConcent],[DatabaseServer],[DatabaseName],[DatabaseUser],[DatabasePwd])
  values ('Gprinter GP-1124T','���Ӵ�-���ӿ����ṩ����֧��',100.0,62.5,2.0,10.0,'D756DAGP8NZ5QUD','NotaryNoData','sa','123456')
End

IF NOT EXISTS (SELECT 1 FROM [ProjectInfo])
Begin
  insert into [ProjectInfo]([ProjectID],[ProjectName],[ProjectShort],[SellerID],[SellerName],[SellerShort],[MinNo],[MaxNo],[StartDate],[EndDate],[ProjectStaus],[CreateDate],[DelStaus])
  values (1,'��������','����',1,'���ŷ���','���ŷ���','000001','999999','2018-05-01 06:00:00','2018-12-01 20:00:00',1,GETDATE(),0)
End

IF NOT EXISTS (SELECT 1 FROM [ProjectLable])
Begin
  insert into [ProjectLable]([ProjectID],[OrderID],[IsBottom],[DelStaus],[LableInfo])
  select 1,1,0,0,'1.�뱣�ܺô��볡Ʊ,����ѡ������Ҫ��֤'
  union all select 1,1,0,0,'2.��ȡ�볡Ʊ��,������ֳ�ָʾ��������'
  union all select 1,1,0,0,'3.��ǰ׼�����Ϲ�������ִ,���֤��������ز���'
  union all select 1,1,0,0,'4.���볡Ʊ��2018��6��28�ŵ�����Ч'
  union all select 1,1,0,0,'5.��������������뼰ʱ��ϵ������ҵ���ʻ��ֳ�������Ա'
  union all select 1,1,1,0,'���Ӵ�-���ӿ����ṩ����֧��'
End

-- select [ID],[PrinterDrive],[PrinterRemark],[PrinterWidth],[PrinterHeight],[PrinterSpeed],[PrinterConcent],[DatabaseServer],[DatabaseName],[DatabaseUser],[DatabasePwd] from [SystemSet]
-- select [ID],[CustomerID],[CustomerName],[CustomerSex],[CustomerCard],[CustomerPhone],[CustomerStaus],[CustomersID],[CreateDate],[DelStaus] from [CustomerInfo]
-- select [ID],[ProjectID],[ProjectName],[ProjectShort],[SellerID],[SellerName],[SellerShort],[MinNo],[MaxNo],[StartDate],[EndDate],[ProjectStaus],[CreateDate],[DelStaus] from [ProjectInfo]
-- select [ID],[ProjectID],[CustomersID],[NotaryNo],[SignNo],[NotaryStaus],[CreateDate],[DelStaus] from [NotaryNoInfo]


 