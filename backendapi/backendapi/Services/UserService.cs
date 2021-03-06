﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backendapi.Models;
using MongoDB.Driver;
using AutoMapper;
using backendapi.DTO;
using Newtonsoft.Json;
using System.Text;
using MongoDB.Bson;

namespace backendapi.Services
{
    public class UserService
    {

        private readonly IMongoCollection<User> _users;
      

        public UserService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>(settings.UserCollectionName);

        }

        public List<User> Get() =>
            _users.Find(user => true).ToList();


        public User Create(User user)
        {

            user.NeedsIds = new List<MongoDB.Bson.ObjectId>();
            _users.InsertOne(user);
            return user;
        }

        public User GetUserByEmail(String email) =>
            _users.Find(user => user.Email == email).FirstOrDefault();

    
        public User GetUser(string id) =>
           _users.Find(user => user.Id == id).FirstOrDefault();

        public List<MongoDB.Bson.ObjectId> GetUserNeedsIds(string id) => 
            _users.Find(user => user.Id == id).FirstOrDefault().NeedsIds;

        public void UpdateUser(string id, User userIn) =>
            _users.FindOneAndReplace(user => user.Id == id, userIn);

        public void AssignUserNeed(string id, ObjectId nid, User userIn) =>
            _users.FindOneAndReplace(user => user.Id == id, userIn);

        public User Login(string email, string password) =>
           _users.Find(user => user.Email == email && user.Password == password).FirstOrDefault();

        public static string SerialGenerator(Object objectToSerialize) =>
            Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(objectToSerialize)));

    }
}
