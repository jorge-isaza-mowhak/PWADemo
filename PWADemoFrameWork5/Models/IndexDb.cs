using Blazor.IndexedDB.Framework;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PWADemoFrameWork5.Models
{
    public class IndexDb : IndexedDb
    {
        public IndexDb(IJSRuntime JSRuntime, string name, int version) : base(JSRuntime, name, version) { }

        public IndexedSet<Employee> Employees { get; set; }
    }

    public class Employee
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
