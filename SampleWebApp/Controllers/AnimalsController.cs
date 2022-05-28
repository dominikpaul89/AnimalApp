using SampleWebApp.Animals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace SampleWebApp
{ 
[Route("api/animals")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private static IConfiguration _configuration;
        public AnimalsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        SqlCommand com = new SqlCommand(); 
        SqlDataReader dr;
        List<Animal> Animalslist = new List<Animal>();

        [HttpGet]
        public IActionResult GetAnimals(string orderBy)
        {
            
           SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb"));
            
            if (orderBy == "IdAnimal")
            {
                throw new Exception("Sorting disabled");
             
            }
            else if (orderBy == null)
            {
                orderBy = "name";
            }
            
          
            
            com.CommandText = "SELECT * FROM Animal";
           
            //com.Parameters.AddWithValue("@orderBy", orderBy);
            com.Connection = con;
            con.Open();
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                Animalslist.Add(new Animal
                {
                    IdAnimal = dr["IdAnimal"].ToString(),
                    Name = dr["Name"].ToString(),
                    Description = dr["Description"].ToString(),
                    Category = dr["Category"].ToString(),
                    Area = dr["Area"].ToString()
                });
            }
            com.Dispose();
            return Ok(Animalslist);
        }
        
        [HttpPost]
        public IActionResult AddAnimal(Animal animal)
        {
            con.Open();
            com.CommandText = "insert into Animal(Name, Description, Category, Area) values('@animal.Name''@animal.Description''@animal.Category''@animal.Area');";
            com.Parameters.AddWithValue("@animal.Name", animal.Name);
            com.Parameters.AddWithValue("@animal.Description", animal.Description);
            com.Parameters.AddWithValue("@animal.Category", animal.Category);
            com.Parameters.AddWithValue("@animal.Area", animal.Area);
            com.ExecuteNonQuery();
            com.Dispose();
            return Ok();
        }
        [HttpPut("{IdAnimal}")]
        public IActionResult UpdateAnimal(string IdAnimal, Animal animal)
        {
            con.Open();
            foreach (Animal i in Animalslist)
            {
                if (i.IdAnimal == IdAnimal)
                {
                    Animal a = i;
                    a.IfValuesAreNull(a, animal);
                    com.CommandText = "UPDATE Animal Set Name = '@animal.Name''@animal.Description''@animal.Category''@animal.Area';";
                    com.Parameters.AddWithValue("@animal.Name", animal.Name);
                    com.Parameters.AddWithValue("@animal.Description", animal.Description);
                    com.Parameters.AddWithValue("@animal.Category", animal.Category);
                    com.Parameters.AddWithValue("@animal.Area", animal.Area);
                    com.ExecuteNonQuery();
                    com.Dispose();
                }
            }
            return Ok();
        }
        [HttpDelete("{IdAnimal}")]
        public IActionResult DeletAnimal(string IdAnimal)
        {
            con.Open();
            foreach (Animal i in Animalslist)
            {
                if (i.IdAnimal == IdAnimal)
                {
                    Animal a = i;
                    com.CommandText = "DELETE FROM Animal WHERE IdAnimal = '@IdAnimal'";
                    com.Parameters.AddWithValue("@IdAnimal", IdAnimal);
                }
            }
            return Ok();
        }
        
    }
}