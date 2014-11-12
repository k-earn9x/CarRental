namespace CarRentalWebService.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using CarRentalWebService.Models;
    using System.Collections.Generic;

    public class DbContextModel : DbContext
    {
        // Your context has been configured to use a 'DbContextModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CarRentalWebService.Models.DbContextModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DbContextModel' 
        // connection string in the application configuration file.
        public DbContextModel()
            : base("name=DbContextModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public virtual DbSet<CarBrand> CarBrands { get; set; }

        public virtual DbSet<City> Cities { get; set; }

        public virtual DbSet<CarFeature> CarFeatures { get; set; }

        public virtual DbSet<CarModel> CarModels { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }

        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }

    public class Initializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<DbContextModel>
    {
        protected override void Seed(DbContextModel context)
        {
            var Brands = new List<CarBrand>
            {
                new CarBrand{Name = "Toyota"},
                new CarBrand{Name = "Suzuki"},
                new CarBrand{Name = "Honda"},
                new CarBrand{Name = "HuynDai"}
            };
            Brands.ForEach(b => context.CarBrands.Add(b));
            context.SaveChanges();

            var Cities = new List<City>
            {
                new City{Name = "Hồ Chí Minh", ZipCode = 700000},
                new City{Name = "Hà Nội", ZipCode = 100000},
                new City{Name = "Bà Rịa Vũng Tàu", ZipCode = 790000},
                new City{Name = "Bạc Liêu", ZipCode = 260000},
                new City{Name = "Bến Tre", ZipCode = 930000},
                new City{Name = "Bình Dương", ZipCode = 590000},
                new City{Name = "Cần Thơ", ZipCode = 270000},
                new City{Name = "Đà Nẵng", ZipCode = 550000},
                new City{Name = "Khánh Hoà", ZipCode = 650000},
                new City{Name = "Long An", ZipCode = 850000},
                new City{Name = "Nghệ An", ZipCode = 470000},
                new City{Name = "Thừa Thiên Huế", ZipCode = 530000},
                new City{Name = "Vĩnh Long", ZipCode = 890000}
            };
            Cities.ForEach(c => context.Cities.Add(c));
            context.SaveChanges();

            var Models = new List<CarModel>
            {
                new CarModel{Name = "Prius", Aircondition = true, AutoTransmission = true, Seats = 4, Doors = 4, LargeBags = 1, SmallBags = 1,
                    PricePerDay = 150, PricePerHour = 15, Brand_Id = 1, Quantity = 10,},
                new CarModel{Name = "Tacoma", Aircondition = true, AutoTransmission = true, Seats = 4, Doors = 4,  LargeBags = 1, SmallBags = 1,
                    PricePerDay = 170, PricePerHour = 17, Brand_Id = 1, Quantity = 10,},
                new CarModel{Name = "Corolla", Aircondition = true, AutoTransmission = true, Seats = 4, Doors = 4,  LargeBags = 1, SmallBags = 1,
                    PricePerDay = 200, PricePerHour = 20, Brand_Id = 1, Quantity = 10,},
                new CarModel{Name = "Yaris", Aircondition = true, AutoTransmission = true, Seats = 4, Doors = 4,  LargeBags = 1, SmallBags = 1,
                    PricePerDay = 140, PricePerHour = 14, Brand_Id = 1, Quantity = 10,},
                new CarModel{Name = "Camry", Aircondition = true, AutoTransmission = true, Seats = 4, Doors = 4,  LargeBags = 1, SmallBags = 1,
                    PricePerDay = 210, PricePerHour = 21, Brand_Id = 1, Quantity = 10,},
                new CarModel{Name = "RAV4", Aircondition = true, AutoTransmission = true, Seats = 4, Doors = 4,  LargeBags = 1, SmallBags = 1,
                    PricePerDay = 140, PricePerHour = 14, Brand_Id = 1, Quantity = 10,},
                new CarModel{Name = "Venza", Aircondition = true, AutoTransmission = true, Seats = 4, Doors = 4,  LargeBags = 1, SmallBags = 1,
                    PricePerDay = 300, PricePerHour = 30, Brand_Id = 1, Quantity = 10,},
                new CarModel{Name = "Accord", Aircondition = true, AutoTransmission = true, Seats = 4, Doors = 4,  LargeBags = 1, SmallBags = 1,
                    PricePerDay = 140, PricePerHour = 14, Brand_Id = 3, Quantity = 10,},
                new CarModel{Name = "Civic", Aircondition = true, AutoTransmission = true, Seats = 4, Doors = 4,  LargeBags = 1, SmallBags = 1,
                    PricePerDay = 300, PricePerHour = 30, Brand_Id = 3, Quantity = 10,},
                new CarModel{Name = "Crosstour", Aircondition = true, AutoTransmission = true, Seats = 4, Doors = 4,  LargeBags = 1, SmallBags = 1,
                    PricePerDay = 200, PricePerHour = 20, Brand_Id = 3, Quantity = 10,},
                new CarModel{Name = "CR-V", Aircondition = true, AutoTransmission = true, Seats = 4, Doors = 4,  LargeBags = 1, SmallBags = 1,
                    PricePerDay = 170, PricePerHour = 17, Brand_Id = 3, Quantity = 10,},
                new CarModel{Name = "Fit", Aircondition = true, AutoTransmission = true, Seats = 4, Doors = 4,  LargeBags = 1, SmallBags = 1,
                    PricePerDay = 140, PricePerHour = 14, Brand_Id = 3, Quantity = 10,},
            };
            Models.ForEach(m => context.CarModels.Add(m));
            context.SaveChanges();

            var Features = new List<CarFeature>
            {
                new CarFeature{Name = "Theft Protection"},
                new CarFeature{Name = "Amendments"},
                new CarFeature{Name = "Collision Damage Waiver"}
            };
            Features.ForEach(f => context.CarFeatures.Add(f));
            context.SaveChanges();
            //base.Seed(context);
            var users = new List<User>
            {
                new User{Name="Nguyen Hoang Nam", userName="hoangnam", password="462617", email="hoangnam19922010@gmail.com", Admin=true},
                new User{Name="Vo Hoang Dang Khoa", userName="dangkhoa", password="123456", email="hoangnam19922010@gmail.com", Admin=true},
                new User{Name="Ho Phuoc Loi", userName="phuocloi", password="123456", email="hoangnam19922010@gmail.com", Admin=false}
            };
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();
        }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}