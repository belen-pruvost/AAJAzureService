using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Web.Http;
using AAJAzureService.DataObjects;
using AAJAzureService.Models;
using Microsoft.WindowsAzure.Mobile.Service;
using System.Data.Entity.Validation;

namespace AAJAzureService
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            
            Database.SetInitializer(new MobileServiceInitializer());
            

        }
    }

    public class MobileServiceInitializer : DropCreateDatabaseIfModelChanges<MobileServiceContext>
    {
        protected override void Seed(MobileServiceContext context)
        {
            List<Visibility> visibilities = new List<Visibility>
            {
                new Visibility { Id = Guid.NewGuid().ToString(), Name = "Internal", Description = "Only AAJ employees users can see" },
                new Visibility { Id = Guid.NewGuid().ToString(), Name = "External", Description = "Only registered users can see" },
                new Visibility { Id = Guid.NewGuid().ToString(), Name = "InternalAndExternal", Description = "Employees and external users can see" }
            };

            foreach (Visibility visibility in visibilities)
            {
                context.Set<Visibility>().Add(visibility);
            }

            List<UserType> userTypes = new List<UserType>
            {
                new UserType { Id = Guid.NewGuid().ToString(), Name = "Employee", Description = "AAJ Employee user"},
                new UserType { Id = Guid.NewGuid().ToString(), Name = "Registered", Description = "User registered in the app that is not an AAJ Employee" }
            };

            foreach (UserType userType in userTypes)
            {
                context.Set<UserType>().Add(userType);
            }

            List<AnnouncementCategory> announcementCategories = new List<AnnouncementCategory>
            {
                new AnnouncementCategory { Id = Guid.NewGuid().ToString(), Name = "Company", Description = "Corporate Announcements related to AAJ" },
                new AnnouncementCategory { Id = Guid.NewGuid().ToString(), Name = "Marketing", Description = "Announcements related to new products" },
                new AnnouncementCategory { Id = Guid.NewGuid().ToString(), Name = "Employees", Description = "Announcements related to AAJ Staff" },
                new AnnouncementCategory { Id = Guid.NewGuid().ToString(), Name = "Sales", Description = "Announcements related to AAJ Sales Department" },
                new AnnouncementCategory { Id = Guid.NewGuid().ToString(), Name = "Technologies", Description = "Announcements related to emerging technologies" },
                new AnnouncementCategory { Id = Guid.NewGuid().ToString(), Name = "Other", Description = "Other Announcements" }
            };

            foreach (AnnouncementCategory announcementCategory in announcementCategories)
            {
                context.Set<AnnouncementCategory>().Add(announcementCategory);
            }

            List<ChatStatus> chatStatuses = new List<ChatStatus>
            {
                new ChatStatus { Id = Guid.NewGuid().ToString(), Name = "Online", Description = "User is online" },
                new ChatStatus { Id = Guid.NewGuid().ToString(), Name = "Offline", Description = "User is offline" }
            };

            foreach (ChatStatus chatStatus in chatStatuses)
            {
                context.Set<ChatStatus>().Add(chatStatus);
            }

            List<Announcement> announcements = new List<Announcement>
            {
                new Announcement {
                    Id = Guid.NewGuid().ToString(), Title = "CEO Visits Argentina",
                    Description = "CEO Amjad Shamim spends some quality time with the AAJ team in Argentina.",
                    ImageUrl = "imgAnnouncement1.jpg", LikesCount = 20,
                    Category = announcementCategories[0], AnnouncementCategoryId = announcementCategories[0].Id,
                    Visibility = visibilities[2], VisibilityId = visibilities[2].Id
                },
                new Announcement
                {
                    Id = Guid.NewGuid().ToString(), Title = "Xamarin Evolve 2016",
                    Description = "Join Xamarin co-founders Nat Friedman and Miguel de Icaza to see what is coming next for"
                                 + " mobile development and the future of apps.",
                    ImageUrl = "imgAnnouncement2.jpg",
                    LikesCount = 15,
                    Category = announcementCategories[4], AnnouncementCategoryId = announcementCategories[4].Id,
                    Visibility = visibilities[0], VisibilityId = visibilities[0].Id
                },
                new Announcement
                {
                    Id = Guid.NewGuid().ToString(), Title = "AAJ App is about to be released!",
                    Description = "Our company app, developed by an argentinian team of developers, is about to be released.",
                    ImageUrl = "imgAnnouncement3.jpg",
                    LikesCount = 20,
                    Visibility = visibilities[1], VisibilityId = visibilities[1].Id,
                    Category = announcementCategories[1], AnnouncementCategoryId = announcementCategories[1].Id
                    
                }
            };

            foreach (Announcement announcement in announcements)
            {
                context.Set<Announcement>().Add(announcement);
            }

            List<User> users = new List<User>
            {
                new User { Id = Guid.NewGuid().ToString(), Email = "jsmith@gmail.com", FirstName = "John", LastName="Smith",
                    Password ="123".GetHashCode().ToString(), ImageUrl="user1.jpg", BirthDay = System.DateTime.Parse("1985/10/10"),
                    ChatStatus = chatStatuses[0], UserType = userTypes[1]
                },
                new User { Id = Guid.NewGuid().ToString(), Email = "alincoln@yahoo.com", FirstName = "Abraham", LastName="Lincoln",
                    Password ="123".GetHashCode().ToString(), ImageUrl="user2.jpg", BirthDay = System.DateTime.Parse("1963/5/3"),
                    ChatStatus = chatStatuses[1], UserType = userTypes[1]
                },
                new User { Id = Guid.NewGuid().ToString(), Email = "fulanito.cosme@aajtech.com", FirstName = "Fulanito", LastName="Cosme",
                    Password ="123".GetHashCode().ToString(), ImageUrl="user3.jpg", BirthDay = System.DateTime.Parse("1990/3/25"),
                    ChatStatus = chatStatuses[0], UserType = userTypes[0]
                }
            };

            foreach (User user in users)
            {
                context.Set<User>().Add(user);
            }

            List<User> receivers1 = new List<User>();
            receivers1.Add(users[1]);
            receivers1.Add(users[2]);

            List<User> receivers2 = new List<User>();
            receivers2.Add(users[1]);

            List<Message> messages = new List<Message>
            {
                new Message { Id = Guid.NewGuid().ToString(), Text = "Hi, how are you?", SenderId = users[0].Id,
                    Sender = users[0], Receivers = receivers1
                },
                new Message { Id = Guid.NewGuid().ToString(), Text = "Good morning Abraham!", SenderId = users[2].Id,
                    Sender = users[2], Receivers = receivers2
                }
            };

            foreach (Message message in messages)
            {
                context.Set<Message>().Add(message);
            }

            base.Seed(context);
        }
    }
}

