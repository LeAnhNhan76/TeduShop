namespace TeduShop.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;
    using TeduShop.Common.Constants;
    using TeduShop.Data;
    using TeduShop.Model.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TeduShop.Data.TeduShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TeduShopDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            CreateUserSample(context);
            CreateProductCategorySample(context);
            CreateSlideSample(context);
            CreatePage(context);
            CreateFooterSample(context);
            CreateContactDetail(context);
            CreateClient(context);
        }

        private void CreateUserSample(TeduShopDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new TeduShopDbContext()));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new TeduShopDbContext()));
            var user = new ApplicationUser()
            {
                UserName = "lanhan",
                Email = "lenhanasus4@gmail.com",
                EmailConfirmed = true,
                Birthday = DateTime.Parse("10/9/1997"),
                FullName = "Lê Anh Nhân"
            };

            manager.Create(user, "lanhan@123");
            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new ApplicationRole() { Name = "Admin" });
                roleManager.Create(new ApplicationRole() { Name = "User" });
            }

            var adminUser = manager.FindByEmail("lenhanasus4@gmail.com");
            manager.AddToRoles(adminUser?.Id, new string[] { "Admin", "User" });
        }

        private void CreateProductCategorySample(TeduShopDbContext context)
        {
            if (!context.ProductCategories.Any())
            {
                var productCategoryList = new List<ProductCategory>()
                {
                    new ProductCategory(){ Name = "Mỹ phẩm", Alias="my-pham", HomeFlag = true, CreatedDate = DateTime.Now, CreatedBy = "admin", UpdatedDate = DateTime.Now, UpdatedBy = "admin" },
                    new ProductCategory(){ Name = "Thực phẩm", Alias="thuc-pham", HomeFlag = true, CreatedDate = DateTime.Now, CreatedBy = "admin", UpdatedDate = DateTime.Now, UpdatedBy = "admin" },
                    new ProductCategory(){ Name = "Quần áo", Alias="quan-ao", HomeFlag = true, CreatedDate = DateTime.Now, CreatedBy = "admin", UpdatedDate = DateTime.Now, UpdatedBy = "admin" },
                    new ProductCategory(){ Name = "Giày dép", Alias="giay-dep", HomeFlag = true, CreatedDate = DateTime.Now, CreatedBy = "admin", UpdatedDate = DateTime.Now, UpdatedBy = "admin" },
                };
                context.ProductCategories.AddRange(productCategoryList);
                context.SaveChanges();
            }
        }

        private void CreateFooterSample(TeduShopDbContext context)
        {
            if (!context.Footers.Any(x => x.ID == Constant.DefaultFooterId))
            {
                var footer = new Footer()
                {
                    ID = Constant.DefaultFooterId,
                    Content = "This is footer section"
                };
                context.Footers.Add(footer);
                context.SaveChanges();
            }
        }

        private void CreateSlideSample(TeduShopDbContext context)
        {
            if (!context.Slides.Any())
            {
                List<Slide> listSlide = new List<Slide>()
                {
                    new Slide() {
                        Name ="Slide 1",
                        DisplayOrder =1,
                        Status =true,
                        URL ="#",
                        Image ="/Assets/client/images/bag.jpg",
                        Content =@"	<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur
                            adipisicing elit, sed do eiusmod tempor incididunt ut labore et </ p >
                        <span class=""on-get"">GET NOW</span>" },
                    new Slide() {
                        Name ="Slide 2",
                        DisplayOrder =2,
                        Status =true,
                        URL ="#",
                        Image ="/Assets/client/images/bag1.jpg",
                        Content=@"<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </ p >
                                <span class=""on-get"">GET NOW</span>"},
                };
                context.Slides.AddRange(listSlide);
                context.SaveChanges();
            }
        }

        private void CreatePage(TeduShopDbContext context)
        {
            if (!context.Pages.Any())
            {
                try
                {
                    var page = new Page()
                    {
                        Name = "Giới thiệu",
                        Alias = "gioi-thieu",
                        Content = @"Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium ",
                        Status = true
                    };
                    context.Pages.Add(page);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }
                }
            }
        }

        private void CreateContactDetail(TeduShopDbContext context)
        {
            if (!context.ContactDetails.Any())
            {
                try
                {
                    var contactDetail = new TeduShop.Model.Models.ContactDetail()
                    {
                        Name = "Shop thời trang TEDU",
                        Address = "23 Nguyễn Thị Huỳnh, phường 8, quận Phú Nhuận",
                        Email = "lenhanasus4@gmai.com",
                        Lat = 10.7969016,
                        Lng = 106.6741883,
                        Phone = "0339700824",
                        Website = "http://tedu.com.vn",
                        Other = "",
                        Status = true
                    };
                    context.ContactDetails.Add(contactDetail);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }
                }
            }
        }

        private void CreateClient(TeduShopDbContext context)
        {
            if (!context.Clients.Any())
            {
                var clients = new List<Client>()
                {
                    new Client
                    {
                        ClientId = "BIG_SHOPE",
                        ClientSecret = "9ebdca36-9c0a-4c57-8dc6-2636af8777aa",
                        ClientName = "Big Shope",
                        CreatedDate = DateTime.Now,
                        RefreshTokenLifeTime = 7200,
                        AllowedOrigin = "*",
                        IsActive = true
                    },
                    new Client
                    {
                        ClientId = "Grab",
                        ClientSecret = "75c114ed-cbfb-483a-a998-05cb27f376a8",
                        ClientName = "Grab",
                        CreatedDate = DateTime.Now,
                        RefreshTokenLifeTime = 7200,
                        AllowedOrigin = "*",
                        IsActive = true
                    }
                };
                context.Clients.AddRange(clients);
                context.SaveChanges();
            }
        }
    }
}