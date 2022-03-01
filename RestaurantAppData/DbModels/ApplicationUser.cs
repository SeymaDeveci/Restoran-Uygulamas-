using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RestaurantAppData.DbModels
{
    public class ApplicationUser : IdentityUser // Microsoftun bize sunduğu ana classlarını kullanmak için 
                                                // IdentityUser iplementasyonu gerçekleştirildi. AspNetUser diye bir tabloyu bize verir.
    {
        [Column(TypeName="nvarchar(150)")] //150 karakterli
        public string FullName { get; set; } //tabloya ekleyeceğimiz özellikler
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
