namespace NewForumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Academies",
                c => new
                    {
                        AcademyID = c.Int(nullable: false, identity: true),
                        AcademyName = c.String(),
                    })
                .PrimaryKey(t => t.AcademyID);
            
            CreateTable(
                "dbo.Blocks",
                c => new
                    {
                        BlockID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        BlockerUserID = c.Int(nullable: false),
                        BlockedUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BlockID)
                .ForeignKey("dbo.Users", t => t.BlockedUserID)
                .ForeignKey("dbo.Users", t => t.BlockerUserID)
                .Index(t => t.BlockerUserID)
                .Index(t => t.BlockedUserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        AcademyID = c.Int(nullable: false),
                        Salt = c.Int(),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Academies", t => t.AcademyID)
                .Index(t => t.AcademyID);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        PictureID = c.Int(nullable: false, identity: true),
                        PictureName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        Type = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PictureID)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        SubjectID = c.Int(nullable: false, identity: true),
                        SubjectName = c.String(nullable: false),
                        LectureType = c.Int(nullable: false),
                        MustAttend = c.Boolean(nullable: false),
                        AcademyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubjectID)
                .ForeignKey("dbo.Academies", t => t.AcademyID)
                .Index(t => t.AcademyID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        IsLocked = c.Boolean(nullable: false),
                        ModerateTopics = c.Boolean(nullable: false),
                        ModeratePosts = c.Boolean(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        PageTitle = c.String(),
                        Path = c.String(),
                        MetaDescription = c.String(),
                        Colour = c.String(),
                        Image = c.String(),
                        ParentCategoryID = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID)
                .ForeignKey("dbo.Categories", t => t.ParentCategoryID)
                .Index(t => t.ParentCategoryID);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        TopicID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Solved = c.Boolean(nullable: false),
                        SolvedReminderSent = c.Boolean(),
                        Slug = c.String(),
                        Views = c.Int(nullable: false),
                        IsSticky = c.Boolean(nullable: false),
                        IsLocked = c.Boolean(nullable: false),
                        LastPostID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        PollID = c.Int(nullable: false),
                        Pending = c.Boolean(),
                    })
                .PrimaryKey(t => t.TopicID)
                .ForeignKey("dbo.Categories", t => t.CategoryID)
                .ForeignKey("dbo.Posts", t => t.LastPostID)
                .ForeignKey("dbo.Polls", t => t.PollID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.LastPostID)
                .Index(t => t.CategoryID)
                .Index(t => t.UserID)
                .Index(t => t.PollID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        PostContent = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        VoteCount = c.Int(nullable: false),
                        DateEdited = c.DateTime(nullable: false),
                        IsSolution = c.Boolean(nullable: false),
                        IsTopicStarter = c.Boolean(nullable: false),
                        FlaggedAsSpam = c.Boolean(),
                        IpAddress = c.String(),
                        Pending = c.Boolean(),
                        TopicID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Topic_TopicID = c.Int(),
                        Tag_TagID = c.Int(),
                    })
                .PrimaryKey(t => t.PostID)
                .ForeignKey("dbo.Topics", t => t.TopicID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .ForeignKey("dbo.Topics", t => t.Topic_TopicID)
                .ForeignKey("dbo.Tags", t => t.Tag_TagID)
                .Index(t => t.TopicID)
                .Index(t => t.UserID)
                .Index(t => t.Topic_TopicID)
                .Index(t => t.Tag_TagID);
            
            CreateTable(
                "dbo.UploadFiles",
                c => new
                    {
                        UploadFileID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Post_PostID = c.Int(),
                    })
                .PrimaryKey(t => t.UploadFileID)
                .ForeignKey("dbo.Posts", t => t.Post_PostID)
                .Index(t => t.Post_PostID);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        VoteID = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        VotedByUserID = c.Int(nullable: false),
                        PostID = c.Int(nullable: false),
                        DateVoted = c.DateTime(),
                    })
                .PrimaryKey(t => t.VoteID)
                .ForeignKey("dbo.Posts", t => t.PostID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .ForeignKey("dbo.Users", t => t.VotedByUserID)
                .Index(t => t.UserID)
                .Index(t => t.VotedByUserID)
                .Index(t => t.PostID);
            
            CreateTable(
                "dbo.Polls",
                c => new
                    {
                        PollID = c.Int(nullable: false, identity: true),
                        IsClosed = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        ClosePollAfterDays = c.Int(),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PollID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.PollAnswers",
                c => new
                    {
                        PollAnswerID = c.Int(nullable: false, identity: true),
                        Answer = c.String(nullable: false),
                        PollID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PollAnswerID)
                .ForeignKey("dbo.Polls", t => t.PollID)
                .Index(t => t.PollID);
            
            CreateTable(
                "dbo.PollVotes",
                c => new
                    {
                        PollVoteID = c.Int(nullable: false, identity: true),
                        PollAnswerID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PollVoteID)
                .ForeignKey("dbo.PollAnswers", t => t.PollAnswerID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.PollAnswerID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileID = c.Int(nullable: false, identity: true),
                        FileName = c.String(nullable: false),
                        Type = c.Int(nullable: false),
                        UploadDate = c.DateTime(nullable: false),
                        SubjectID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileID)
                .ForeignKey("dbo.Subjects", t => t.SubjectID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.SubjectID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        LanguageID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        LanguageCulture = c.String(nullable: false),
                        FlagImageFileName = c.String(nullable: false),
                        RightToLeft = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LanguageID);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        LogID = c.Int(nullable: false, identity: true),
                        EventDateTime = c.DateTime(nullable: false),
                        EventLevel = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        MachineName = c.String(nullable: false),
                        EventMessage = c.String(nullable: false),
                        ErrorSource = c.String(),
                        ErrorClass = c.String(),
                        ErrorMethod = c.String(),
                        ErrorMessage = c.String(),
                        InnerErrorMessage = c.String(),
                    })
                .PrimaryKey(t => t.LogID);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        SettingsID = c.Int(nullable: false, identity: true),
                        ForumName = c.String(),
                        ForumUrl = c.String(),
                        PageTitle = c.String(),
                        MetaDesc = c.String(),
                        IsClosed = c.Boolean(nullable: false),
                        EnableRSSFeeds = c.Boolean(nullable: false),
                        DisplayEditedBy = c.Boolean(nullable: false),
                        EnablePostFileAttachments = c.Boolean(nullable: false),
                        EnableMarkAsSolution = c.Boolean(nullable: false),
                        MarkAsSolutionReminderTimeFrame = c.Int(),
                        EnableSpamReporting = c.Boolean(nullable: false),
                        EnableMemberReporting = c.Boolean(nullable: false),
                        EnableEmailSubscriptions = c.Boolean(nullable: false),
                        ManuallyAuthoriseNewMembers = c.Boolean(nullable: false),
                        NewMemberEmailConfirmation = c.Boolean(),
                        EmailAdminOnNewMemberSignUp = c.Boolean(nullable: false),
                        TopicsPerPage = c.Int(nullable: false),
                        PostsPerPage = c.Int(nullable: false),
                        ActivitiesPerPage = c.Int(nullable: false),
                        EnablePrivateMessages = c.Boolean(nullable: false),
                        MaxPrivateMessagesPerMember = c.Int(nullable: false),
                        PrivateMessageFloodControl = c.Int(nullable: false),
                        EnableSignatures = c.Boolean(nullable: false),
                        EnablePoints = c.Boolean(nullable: false),
                        PointsAllowedForExtendedProfile = c.Int(),
                        PointsAllowedToVoteAmount = c.Int(nullable: false),
                        PointsAddedPerPost = c.Int(nullable: false),
                        PointsAddedPostiveVote = c.Int(nullable: false),
                        PointsDeductedNagativeVote = c.Int(nullable: false),
                        PointsAddedForSolution = c.Int(nullable: false),
                        AdminEmailAddress = c.String(),
                        NotificationReplyEmail = c.String(),
                        SMTP = c.String(),
                        SMTPUsername = c.String(),
                        SMTPPassword = c.String(),
                        SMTPPort = c.String(),
                        SMTPEnableSSL = c.Boolean(),
                        Theme = c.String(),
                        EnableSocialLogins = c.Boolean(),
                        SpamQuestion = c.String(),
                        SpamAnswer = c.String(),
                        EnableAkisment = c.Boolean(),
                        AkismentKey = c.String(),
                        CurrentDatabaseVersion = c.String(),
                        EnablePolls = c.Boolean(),
                        SuspendRegistration = c.Boolean(),
                        CustomHeaderCode = c.String(),
                        CustomFooterCode = c.String(),
                        EnableEmoticons = c.Boolean(),
                        DisableDislikeButton = c.Boolean(nullable: false),
                        AgreeToTermsAndConditions = c.Boolean(),
                        TermsAndConditions = c.String(),
                        DisableStandardRegistration = c.Boolean(),
                        RoleID = c.Int(nullable: false),
                        LanguageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SettingsID)
                .ForeignKey("dbo.Languages", t => t.LanguageID)
                .ForeignKey("dbo.Roles", t => t.RoleID)
                .Index(t => t.RoleID)
                .Index(t => t.LanguageID);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.TagID);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.RoleID })
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.UserSubjects",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        SubjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.SubjectID })
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.SubjectID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "Tag_TagID", "dbo.Tags");
            DropForeignKey("dbo.Settings", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.Settings", "LanguageID", "dbo.Languages");
            DropForeignKey("dbo.Files", "UserID", "dbo.Users");
            DropForeignKey("dbo.Files", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.Topics", "UserID", "dbo.Users");
            DropForeignKey("dbo.Posts", "Topic_TopicID", "dbo.Topics");
            DropForeignKey("dbo.Topics", "PollID", "dbo.Polls");
            DropForeignKey("dbo.Polls", "UserID", "dbo.Users");
            DropForeignKey("dbo.PollVotes", "UserID", "dbo.Users");
            DropForeignKey("dbo.PollVotes", "PollAnswerID", "dbo.PollAnswers");
            DropForeignKey("dbo.PollAnswers", "PollID", "dbo.Polls");
            DropForeignKey("dbo.Topics", "LastPostID", "dbo.Posts");
            DropForeignKey("dbo.Votes", "VotedByUserID", "dbo.Users");
            DropForeignKey("dbo.Votes", "UserID", "dbo.Users");
            DropForeignKey("dbo.Votes", "PostID", "dbo.Posts");
            DropForeignKey("dbo.Posts", "UserID", "dbo.Users");
            DropForeignKey("dbo.Posts", "TopicID", "dbo.Topics");
            DropForeignKey("dbo.UploadFiles", "Post_PostID", "dbo.Posts");
            DropForeignKey("dbo.Topics", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Categories", "ParentCategoryID", "dbo.Categories");
            DropForeignKey("dbo.Blocks", "BlockerUserID", "dbo.Users");
            DropForeignKey("dbo.Blocks", "BlockedUserID", "dbo.Users");
            DropForeignKey("dbo.UserSubjects", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.UserSubjects", "UserID", "dbo.Users");
            DropForeignKey("dbo.Subjects", "AcademyID", "dbo.Academies");
            DropForeignKey("dbo.UserRoles", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "UserID", "dbo.Users");
            DropForeignKey("dbo.Pictures", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "AcademyID", "dbo.Academies");
            DropIndex("dbo.UserSubjects", new[] { "SubjectID" });
            DropIndex("dbo.UserSubjects", new[] { "UserID" });
            DropIndex("dbo.UserRoles", new[] { "RoleID" });
            DropIndex("dbo.UserRoles", new[] { "UserID" });
            DropIndex("dbo.Settings", new[] { "LanguageID" });
            DropIndex("dbo.Settings", new[] { "RoleID" });
            DropIndex("dbo.Files", new[] { "UserID" });
            DropIndex("dbo.Files", new[] { "SubjectID" });
            DropIndex("dbo.PollVotes", new[] { "UserID" });
            DropIndex("dbo.PollVotes", new[] { "PollAnswerID" });
            DropIndex("dbo.PollAnswers", new[] { "PollID" });
            DropIndex("dbo.Polls", new[] { "UserID" });
            DropIndex("dbo.Votes", new[] { "PostID" });
            DropIndex("dbo.Votes", new[] { "VotedByUserID" });
            DropIndex("dbo.Votes", new[] { "UserID" });
            DropIndex("dbo.UploadFiles", new[] { "Post_PostID" });
            DropIndex("dbo.Posts", new[] { "Tag_TagID" });
            DropIndex("dbo.Posts", new[] { "Topic_TopicID" });
            DropIndex("dbo.Posts", new[] { "UserID" });
            DropIndex("dbo.Posts", new[] { "TopicID" });
            DropIndex("dbo.Topics", new[] { "PollID" });
            DropIndex("dbo.Topics", new[] { "UserID" });
            DropIndex("dbo.Topics", new[] { "CategoryID" });
            DropIndex("dbo.Topics", new[] { "LastPostID" });
            DropIndex("dbo.Categories", new[] { "ParentCategoryID" });
            DropIndex("dbo.Subjects", new[] { "AcademyID" });
            DropIndex("dbo.Pictures", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "AcademyID" });
            DropIndex("dbo.Blocks", new[] { "BlockedUserID" });
            DropIndex("dbo.Blocks", new[] { "BlockerUserID" });
            DropTable("dbo.UserSubjects");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Tags");
            DropTable("dbo.Settings");
            DropTable("dbo.Logs");
            DropTable("dbo.Languages");
            DropTable("dbo.Files");
            DropTable("dbo.PollVotes");
            DropTable("dbo.PollAnswers");
            DropTable("dbo.Polls");
            DropTable("dbo.Votes");
            DropTable("dbo.UploadFiles");
            DropTable("dbo.Posts");
            DropTable("dbo.Topics");
            DropTable("dbo.Categories");
            DropTable("dbo.Subjects");
            DropTable("dbo.Roles");
            DropTable("dbo.Pictures");
            DropTable("dbo.Users");
            DropTable("dbo.Blocks");
            DropTable("dbo.Academies");
        }
    }
}
