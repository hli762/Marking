IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230921112423_InitialCreate')
BEGIN
    CREATE TABLE [Admins] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [UPI] nvarchar(max) NULL,
        [Password] nvarchar(max) NULL,
        CONSTRAINT [PK_Admins] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230921112423_InitialCreate')
BEGIN
    CREATE TABLE [Assessments] (
        [Id] int NOT NULL IDENTITY,
        [CourseId] int NOT NULL,
        [AssessmentDescription] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Assessments] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230921112423_InitialCreate')
BEGIN
    CREATE TABLE [courseSupervisors] (
        [Id] int NOT NULL IDENTITY,
        CONSTRAINT [PK_courseSupervisors] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230921112423_InitialCreate')
BEGIN
    CREATE TABLE [markers] (
        [Id] int NOT NULL IDENTITY,
        CONSTRAINT [PK_markers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230921112423_InitialCreate')
BEGIN
    CREATE TABLE [Semesters] (
        [SemesterID] int NOT NULL IDENTITY,
        [Year] int NOT NULL,
        [SemesterType] nvarchar(max) NULL,
        CONSTRAINT [PK_Semesters] PRIMARY KEY ([SemesterID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230921112423_InitialCreate')
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        [UPI] nvarchar(max) NULL,
        [IsOverseas] bit NOT NULL,
        [IsCitizenOrPR] bit NOT NULL,
        [MarkerEnrolmentDetails] nvarchar(max) NULL,
        [IsPostgraduate] nvarchar(max) NULL,
        [MaxHoursPerWeek] real NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230921112423_InitialCreate')
BEGIN
    CREATE TABLE [Courses] (
        [CourseNumber] int NOT NULL IDENTITY,
        [CourseName] nvarchar(max) NOT NULL,
        [EstimatedStudents] int NOT NULL,
        [EnrolledStudents] int NOT NULL,
        [NeedsMarker] bit NOT NULL,
        [CanPreAssignMarkers] bit NOT NULL,
        [CourseCoordinatorID] int NOT NULL,
        [SemesterID] int NULL,
        [CourseSupervisorId] int NULL,
        CONSTRAINT [PK_Courses] PRIMARY KEY ([CourseNumber]),
        CONSTRAINT [FK_Courses_Semesters_SemesterID] FOREIGN KEY ([SemesterID]) REFERENCES [Semesters] ([SemesterID]),
        CONSTRAINT [FK_Courses_Users_CourseCoordinatorID] FOREIGN KEY ([CourseCoordinatorID]) REFERENCES [Users] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Courses_courseSupervisors_CourseSupervisorId] FOREIGN KEY ([CourseSupervisorId]) REFERENCES [courseSupervisors] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230921112423_InitialCreate')
BEGIN
    CREATE TABLE [Applications] (
        [Id] int NOT NULL IDENTITY,
        [StudentID] int NOT NULL,
        [CourseID] int NOT NULL,
        [GradeObtained] nvarchar(max) NULL,
        [QualificationsExplanation] nvarchar(max) NULL,
        [PreviousExperience] nvarchar(max) NULL,
        [Status] nvarchar(max) NULL,
        [userId] int NULL,
        CONSTRAINT [PK_Applications] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Applications_Courses_CourseID] FOREIGN KEY ([CourseID]) REFERENCES [Courses] ([CourseNumber]) ON DELETE CASCADE,
        CONSTRAINT [FK_Applications_Users_userId] FOREIGN KEY ([userId]) REFERENCES [Users] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230921112423_InitialCreate')
BEGIN
    CREATE TABLE [Assignments] (
        [Id] int NOT NULL IDENTITY,
        [CourseNumber] int NULL,
        CONSTRAINT [PK_Assignments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Assignments_Courses_CourseNumber] FOREIGN KEY ([CourseNumber]) REFERENCES [Courses] ([CourseNumber])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230921112423_InitialCreate')
BEGIN
    CREATE TABLE [CourseMarker] (
        [coursesCourseNumber] int NOT NULL,
        [markersId] int NOT NULL,
        CONSTRAINT [PK_CourseMarker] PRIMARY KEY ([coursesCourseNumber], [markersId]),
        CONSTRAINT [FK_CourseMarker_Courses_coursesCourseNumber] FOREIGN KEY ([coursesCourseNumber]) REFERENCES [Courses] ([CourseNumber]) ON DELETE CASCADE,
        CONSTRAINT [FK_CourseMarker_markers_markersId] FOREIGN KEY ([markersId]) REFERENCES [markers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230921112423_InitialCreate')
BEGIN
    CREATE INDEX [IX_Applications_CourseID] ON [Applications] ([CourseID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230921112423_InitialCreate')
BEGIN
    CREATE INDEX [IX_Applications_userId] ON [Applications] ([userId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230921112423_InitialCreate')
BEGIN
    CREATE INDEX [IX_Assignments_CourseNumber] ON [Assignments] ([CourseNumber]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230921112423_InitialCreate')
BEGIN
    CREATE INDEX [IX_CourseMarker_markersId] ON [CourseMarker] ([markersId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230921112423_InitialCreate')
BEGIN
    CREATE INDEX [IX_Courses_CourseCoordinatorID] ON [Courses] ([CourseCoordinatorID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230921112423_InitialCreate')
BEGIN
    CREATE INDEX [IX_Courses_CourseSupervisorId] ON [Courses] ([CourseSupervisorId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230921112423_InitialCreate')
BEGIN
    CREATE INDEX [IX_Courses_SemesterID] ON [Courses] ([SemesterID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230921112423_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230921112423_InitialCreate', N'7.0.11');
END;
GO

COMMIT;
GO

