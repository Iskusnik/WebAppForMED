
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/31/2018 23:13:59
-- Generated from EDMX file: c:\users\iskusnikxd\documents\visual studio 2015\Projects\WebAppForMED\WebAppForMED\Models\ModelMED.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MyNewMedWeb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_MedCardIllness_MedCard]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MedCardIllness] DROP CONSTRAINT [FK_MedCardIllness_MedCard];
GO
IF OBJECT_ID(N'[dbo].[FK_MedCardIllness_Illness]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MedCardIllness] DROP CONSTRAINT [FK_MedCardIllness_Illness];
GO
IF OBJECT_ID(N'[dbo].[FK_MedCardDocRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DocRecordSet] DROP CONSTRAINT [FK_MedCardDocRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_DoctorDocRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DocRecordSet] DROP CONSTRAINT [FK_DoctorDocRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_DoctorFreeTime]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FreeTimeSet] DROP CONSTRAINT [FK_DoctorFreeTime];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientWorkTime]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WorkTimeSet] DROP CONSTRAINT [FK_PatientWorkTime];
GO
IF OBJECT_ID(N'[dbo].[FK_DoctorWorkTime]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WorkTimeSet] DROP CONSTRAINT [FK_DoctorWorkTime];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientMedCard]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonSet_Patient] DROP CONSTRAINT [FK_PatientMedCard];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[MedCardSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MedCardSet];
GO
IF OBJECT_ID(N'[dbo].[IllnessSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IllnessSet];
GO
IF OBJECT_ID(N'[dbo].[DocRecordSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DocRecordSet];
GO
IF OBJECT_ID(N'[dbo].[FreeTimeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FreeTimeSet];
GO
IF OBJECT_ID(N'[dbo].[WorkTimeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WorkTimeSet];
GO
IF OBJECT_ID(N'[dbo].[PersonSet_Doctor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonSet_Doctor];
GO
IF OBJECT_ID(N'[dbo].[PersonSet_Patient]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonSet_Patient];
GO
IF OBJECT_ID(N'[dbo].[MedCardIllness]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MedCardIllness];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'MedCardSet'
CREATE TABLE [dbo].[MedCardSet] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'IllnessSet'
CREATE TABLE [dbo].[IllnessSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DocRecordSet'
CREATE TABLE [dbo].[DocRecordSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Diagnos] nvarchar(max)  NOT NULL,
    [MedCard_Id] int  NOT NULL,
    [Doctor_Id] int  NOT NULL
);
GO

-- Creating table 'FreeTimeSet'
CREATE TABLE [dbo].[FreeTimeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StartTime] datetime  NOT NULL,
    [Doctor_Id] int  NOT NULL
);
GO

-- Creating table 'WorkTimeSet'
CREATE TABLE [dbo].[WorkTimeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StartTime] datetime  NOT NULL,
    [Patient_Id] int  NOT NULL,
    [Doctor_Id] int  NOT NULL
);
GO

-- Creating table 'DoctorSet'
CREATE TABLE [dbo].[DoctorSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FIO] nvarchar(max)  NOT NULL,
    [Nation] nvarchar(max)  NULL,
    [BirthDate] datetime  NOT NULL,
    [BirthPlace] nvarchar(max)  NULL,
    [LivePlace] nvarchar(max)  NULL,
    [Pol] nvarchar(max)  NOT NULL,
    [Job] nvarchar(max)  NOT NULL,
    [Insurance] nvarchar(max)  NOT NULL,
    [DocType] nvarchar(max)  NOT NULL,
    [DocNum] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PatientSet'
CREATE TABLE [dbo].[PatientSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FIO] nvarchar(max)  NOT NULL,
    [Nation] nvarchar(max)  NULL,
    [BirthDate] datetime  NOT NULL,
    [BirthPlace] nvarchar(max)  NULL,
    [LivePlace] nvarchar(max)  NULL,
    [Pol] nvarchar(max)  NOT NULL,
    [OMS] nvarchar(max)  NOT NULL,
    [Blood] nvarchar(max)  NULL,
    [DocType] nvarchar(max)  NOT NULL,
    [DocNum] nvarchar(max)  NOT NULL,
    [MedCard_Id] int  NOT NULL
);
GO

-- Creating table 'MedCardIllness'
CREATE TABLE [dbo].[MedCardIllness] (
    [MedCard_Id] int  NOT NULL,
    [Illness_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'MedCardSet'
ALTER TABLE [dbo].[MedCardSet]
ADD CONSTRAINT [PK_MedCardSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IllnessSet'
ALTER TABLE [dbo].[IllnessSet]
ADD CONSTRAINT [PK_IllnessSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DocRecordSet'
ALTER TABLE [dbo].[DocRecordSet]
ADD CONSTRAINT [PK_DocRecordSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FreeTimeSet'
ALTER TABLE [dbo].[FreeTimeSet]
ADD CONSTRAINT [PK_FreeTimeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WorkTimeSet'
ALTER TABLE [dbo].[WorkTimeSet]
ADD CONSTRAINT [PK_WorkTimeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DoctorSet'
ALTER TABLE [dbo].[DoctorSet]
ADD CONSTRAINT [PK_DoctorSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PatientSet'
ALTER TABLE [dbo].[PatientSet]
ADD CONSTRAINT [PK_PatientSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [MedCard_Id], [Illness_Id] in table 'MedCardIllness'
ALTER TABLE [dbo].[MedCardIllness]
ADD CONSTRAINT [PK_MedCardIllness]
    PRIMARY KEY CLUSTERED ([MedCard_Id], [Illness_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [MedCard_Id] in table 'MedCardIllness'
ALTER TABLE [dbo].[MedCardIllness]
ADD CONSTRAINT [FK_MedCardIllness_MedCard]
    FOREIGN KEY ([MedCard_Id])
    REFERENCES [dbo].[MedCardSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Illness_Id] in table 'MedCardIllness'
ALTER TABLE [dbo].[MedCardIllness]
ADD CONSTRAINT [FK_MedCardIllness_Illness]
    FOREIGN KEY ([Illness_Id])
    REFERENCES [dbo].[IllnessSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MedCardIllness_Illness'
CREATE INDEX [IX_FK_MedCardIllness_Illness]
ON [dbo].[MedCardIllness]
    ([Illness_Id]);
GO

-- Creating foreign key on [MedCard_Id] in table 'DocRecordSet'
ALTER TABLE [dbo].[DocRecordSet]
ADD CONSTRAINT [FK_MedCardDocRecord]
    FOREIGN KEY ([MedCard_Id])
    REFERENCES [dbo].[MedCardSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MedCardDocRecord'
CREATE INDEX [IX_FK_MedCardDocRecord]
ON [dbo].[DocRecordSet]
    ([MedCard_Id]);
GO

-- Creating foreign key on [Doctor_Id] in table 'DocRecordSet'
ALTER TABLE [dbo].[DocRecordSet]
ADD CONSTRAINT [FK_DoctorDocRecord]
    FOREIGN KEY ([Doctor_Id])
    REFERENCES [dbo].[DoctorSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DoctorDocRecord'
CREATE INDEX [IX_FK_DoctorDocRecord]
ON [dbo].[DocRecordSet]
    ([Doctor_Id]);
GO

-- Creating foreign key on [Doctor_Id] in table 'FreeTimeSet'
ALTER TABLE [dbo].[FreeTimeSet]
ADD CONSTRAINT [FK_DoctorFreeTime]
    FOREIGN KEY ([Doctor_Id])
    REFERENCES [dbo].[DoctorSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DoctorFreeTime'
CREATE INDEX [IX_FK_DoctorFreeTime]
ON [dbo].[FreeTimeSet]
    ([Doctor_Id]);
GO

-- Creating foreign key on [Patient_Id] in table 'WorkTimeSet'
ALTER TABLE [dbo].[WorkTimeSet]
ADD CONSTRAINT [FK_PatientWorkTime]
    FOREIGN KEY ([Patient_Id])
    REFERENCES [dbo].[PatientSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientWorkTime'
CREATE INDEX [IX_FK_PatientWorkTime]
ON [dbo].[WorkTimeSet]
    ([Patient_Id]);
GO

-- Creating foreign key on [Doctor_Id] in table 'WorkTimeSet'
ALTER TABLE [dbo].[WorkTimeSet]
ADD CONSTRAINT [FK_DoctorWorkTime]
    FOREIGN KEY ([Doctor_Id])
    REFERENCES [dbo].[DoctorSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DoctorWorkTime'
CREATE INDEX [IX_FK_DoctorWorkTime]
ON [dbo].[WorkTimeSet]
    ([Doctor_Id]);
GO

-- Creating foreign key on [MedCard_Id] in table 'PatientSet'
ALTER TABLE [dbo].[PatientSet]
ADD CONSTRAINT [FK_PatientMedCard]
    FOREIGN KEY ([MedCard_Id])
    REFERENCES [dbo].[MedCardSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientMedCard'
CREATE INDEX [IX_FK_PatientMedCard]
ON [dbo].[PatientSet]
    ([MedCard_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------