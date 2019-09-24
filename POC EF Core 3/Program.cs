using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POC_EF_Core_3
{
    public abstract class SynchronizableBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("POC EF CORE 3");
        }
    }
}