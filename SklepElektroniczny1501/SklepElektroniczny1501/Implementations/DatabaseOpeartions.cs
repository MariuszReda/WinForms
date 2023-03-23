using Microsoft.EntityFrameworkCore.Metadata;
using SklepElektroniczny1501.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SklepElektroniczny1501.Implementations
{
    public class DatabaseOpeartions : IDatabaseOpeartions
    {
        DtoContex dtoContext = new DtoContex();
        private readonly IModel _model;
        //public DatabaseOpeartions(IModel model)
        //{
        //    _model= model;
        //}
        private List<PropertyInfo> getRelationTable(List<PropertyInfo> propertyInfos) 
        {
            var collectionProperties = new List<PropertyInfo>();
            foreach (var property in propertyInfos)
            {
                if (property.GetGetMethod().IsVirtual)
                {
                    collectionProperties.Add(property);
                }
            }
            return collectionProperties;
        }
        public async Task<List<T>> GetAllAsync<T>() where T : class
        {
            var query = dtoContext.Set<T>().AsQueryable();
            var propertyInfos = typeof(T).GetProperties().ToList();
            var collectionProperties = getRelationTable(propertyInfos);


            foreach (var property in collectionProperties)
            {
                var navigationPropertyName = property.Name;
                query = query.Include(navigationPropertyName);
            }

                return await query.ToListAsync();
        }

        public async Task<bool> AddAsync<T>(T item) where T : class
        {
            dtoContext.Set<T>().Add(item);
            var result = await dtoContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<T> GetItemById<T>(Guid id) where T : class
        {
            return await dtoContext.Set<T>().FindAsync(id);
        }

        public async Task<bool> UpdateAsync<T>(T item) where T : class
        {
            var entry = dtoContext.Entry(item);

            if (entry.State == EntityState.Detached)
            {
                var set = dtoContext.Set<T>();
                T attachedEntity = await set.FindAsync(entry.Property("Id").CurrentValue);
                if (attachedEntity != null)
                {
                    var attachedEntry = dtoContext.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(item);
                }
                else
                {
                    entry.State = EntityState.Modified;
                }
            }

            var result = await dtoContext.SaveChangesAsync();
            return result > 0;
        }

        public async void DeleteAsync<T>(int itemId) where T : class
        {
            var item = await dtoContext.Set<T>().FindAsync(itemId);

            if (item != null)
            {
                dtoContext.Set<T>().Remove(item);
                await dtoContext.SaveChangesAsync();
            }
            //if (entry.State == EntityState.Detached)
            //{
            //    var set = myTestdb.Set<T>();
            //    T attachedEntity = set.Find(entry.Property("Id").CurrentValue);
            //    if (attachedEntity != null)
            //    {
            //        var attachedEntry = myTestdb.Entry(attachedEntity);
            //        attachedEntry.CurrentValues.SetValues(item);
            //    }
            //    else
            //    {
            //        entry.State = EntityState.Modified;
            //    }
            //}

            //myTestdb.SaveChanges();
        }


    }
}