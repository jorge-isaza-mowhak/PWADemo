using Blazor.IndexedDB.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PWADemoFrameWork5.Models
{
    public class ExampleDB : IndexedDb
    {
        public ExampleDB(Microsoft.JSInterop.IJSRuntime jSRuntime, string name, int version) : base(jSRuntime, name, version) { }

        // These are like tables. Declare as many of them as you want.
        public IndexedSet<Person> Employees { get; set; }
    }


    public class Person
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }


}
