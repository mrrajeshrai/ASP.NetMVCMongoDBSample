using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Configuration;
using ASP.NetMVCMongoDBSample.Models;
using MongoDB.Driver.Builders;
using System.Web.Script.Serialization;

namespace ASP.NetMVCMongoDBSample.Controllers
{
    public class HomeController : Controller
    {
        MongoServer objServer = MongoServer.Create("mongodb://localhost/?safe=true");

        public ActionResult Index()
        {

            MongoDatabase objDatabse = objServer.GetDatabase("MVCTestDB");
            List<UserModel> UserDetails = objDatabse.GetCollection<UserModel>("Users").FindAll().ToList();
            return View(UserDetails);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserModel um)
        {


            MongoDatabase objDatabse = objServer.GetDatabase("MVCTestDB");
            MongoCollection<UserModel> collection = objDatabse.GetCollection<UserModel>("Users");

            //Adding the element to the BsonDocument  after reading from UI Element.  

            BsonDocument newstudentInfo = new BsonDocument{
                        {"UserName",um.UserName},
                        {"Password",um.Password},

                         {"Email",um.Email },
                        {"PhoneNo",um.PhoneNo},

                         {"Address",um.Address},

                    };

            collection.Insert(newstudentInfo);

            return RedirectToAction("Index");
        }

        public ActionResult Details(string id)
        {

            MongoDatabase objDatabse = objServer.GetDatabase("MVCTestDB");
            IMongoQuery query = Query.EQ("_id", ObjectId.Parse(id));
            UserModel user = objDatabse.GetCollection<UserModel>("Users").Find(query).SingleOrDefault();

            return View(user);
        }

        public ActionResult Delete(string id)
        {

            MongoDatabase objDatabse = objServer.GetDatabase("MVCTestDB");
            IMongoQuery query = Query.EQ("_id", ObjectId.Parse(id));
            UserModel user = objDatabse.GetCollection<UserModel>("Users").Find(query).SingleOrDefault();
            return View(user);
        }

        [HttpPost]
        public ActionResult Delete(UserModel um)
        {

            string id = ((string[])um._id)[0];

            MongoDatabase objDatabse = objServer.GetDatabase("MVCTestDB");
            IMongoQuery query = Query.EQ("_id", ObjectId.Parse(id));
            objDatabse.GetCollection("Users").Remove(query);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {

            MongoDatabase objDatabse = objServer.GetDatabase("MVCTestDB");
            IMongoQuery query = Query.EQ("_id", ObjectId.Parse(id));
            UserModel user = objDatabse.GetCollection<UserModel>("Users").Find(query).SingleOrDefault();
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(UserModel um)
        {
            string id = ((string[])um._id)[0];
            MongoDatabase objDatabse = objServer.GetDatabase("MVCTestDB");
            IMongoQuery query = Query.EQ("_id", ObjectId.Parse(id));
            IMongoUpdate updateQuery = Update.Set("UserName",
            um.UserName).Set("Password", um.Password).Set("Email", um.Email).Set
            ("PhoneNo", um.PhoneNo).Set("Address", um.Address);
            // UserModel user = objDatabse.GetCollection<UserModel>("Users").Find(query).SingleOrDefault();
            objDatabse.GetCollection("Users").Update(query, updateQuery);
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}