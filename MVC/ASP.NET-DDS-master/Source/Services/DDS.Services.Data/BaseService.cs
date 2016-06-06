﻿namespace DDS.Services.Data
{
    using System;
    using System.Linq;

    using DDS.Data.Common;
    using DDS.Data.Common.Models;
    using Interfaces;

    public class BaseService<T> : IBaseServices<T>
        where T : BaseModel<int>
    {
        private IDbRepository<T> items;

        public BaseService(IDbRepository<T> items)
        {
            this.items = items;
        }

        internal IDbRepository<T> Items
        {
            get
            {
                return this.items;
            }
        }

        /// <summary>
        /// Marks entity of type BaseModel<int> as deleted. Calls Items.Save() method!
        /// </summary>
        /// <param name="entity">Given entity of type BaseModel<int></param>
        public void Delete(T entity)
        {
            this.Items.Delete(entity);
            this.Items.Save();
        }

        /// <summary>
        /// Adds entity of type BaseModel<int>. Calls Items.Save() method!
        /// </summary>
        /// <param name="entity">Given entity of type BaseModel<int></param>
        public void Create(T entity)
        {
            this.Items.Add(entity);
            this.Items.Save();
        }

        /// <summary>
        /// Edits the date and time modified and deleted as well as the IsDeleted property for the entity. Calls Items.Save() method!
        /// </summary>
        /// <param name="entity">Given entity of type BaseModel<int></param>
        public virtual void Edit(T entity)
        {
            this.Items.GetById(entity.Id).ModifiedOn = DateTime.Now;
            this.Items.GetById(entity.Id).IsDeleted = entity.IsDeleted;
            if (entity.IsDeleted)
            {
                this.Items.GetById(entity.Id).DeletedOn = DateTime.Now;
            }
            else
            {
                this.Items.GetById(entity.Id).DeletedOn = null;
            }

            this.Items.Save();
        }

        /// <summary>
        /// Returns entity of type <paramref name="T"/>
        /// </summary>
        /// <param name="id">id of the entity to look for</param>
        /// <returns>Entity of type <paramref name="T"/></returns>
        public T GetById(int id)
        {
            return this.Items.GetById(id);
        }

        /// <summary>
        /// Returns entities of type <paramref name="T"/>
        /// </summary>
        /// <returns>IQueryable of type <paramref name="T"/></returns>
        public IQueryable<T> GetAll()
        {
            return this.Items.All();
        }

        /// <summary>
        /// Returns entities of type <paramref name="T"/>
        /// </summary>
        /// <returns>IQueryable of type <paramref name="T"/></returns>
        public IQueryable<T> GetDeleted()
        {
            return this.Items.AllWithDeleted().Where(x => x.IsDeleted == true);
        }
    }
}
