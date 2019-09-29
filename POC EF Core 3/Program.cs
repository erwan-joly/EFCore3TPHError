using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace POC_EF_Core_3
{
    public abstract class SynchronizableBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
    }

    public static class DbContextFindAllExtensions
    {
        private static readonly MethodInfo ContainsMethod = typeof(Enumerable).GetMethods()
            .FirstOrDefault(m => (m.Name == "Contains") && (m.GetParameters().Length == 2))
            .MakeGenericMethod(typeof(object));

        public static IQueryable<T> FindAllAsync<T>(this DbSet<T> dbSet, PropertyInfo keyProperty,
            params object[] keyValues)
            where T : class
        {
            // build lambda expression
            var parameter = Expression.Parameter(typeof(T), "e");
            var body = Expression.Call(null, ContainsMethod,
                Expression.Constant(keyValues),
                Expression.Convert(Expression.MakeMemberAccess(parameter, keyProperty), typeof(object)));
            var predicateExpression = Expression.Lambda<Func<T, bool>>(body, parameter);

            // run query
            Console.WriteLine(predicateExpression.ToString());
            return dbSet.Where(predicateExpression);
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var context = new PocDbContext())
                {
                    var dbkey = typeof(ItemInstance).GetProperty("Id");

                    var test = context.ItemInstance.FindAllAsync(dbkey, new[] { new Guid() }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                using (var context = new PocDbContext())
                {
                    Expression<Func<ItemInstance, bool>> predicateExpression = s => new[] {new Guid()}.Contains(s.Id);
                    Console.WriteLine(predicateExpression.ToString());
                    var test = context.ItemInstance.Where(predicateExpression).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            } 
            Console.ReadKey();
        }
    }
}