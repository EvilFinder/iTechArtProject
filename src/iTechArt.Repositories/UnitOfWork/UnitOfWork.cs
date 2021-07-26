﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iTechArt.Common;
using iTechArt.Repositories.Repository;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.Repositories.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        protected readonly TContext _dbContext;
        protected readonly ILog _logger;

        protected readonly Dictionary<Type, object> _repositories;
        protected readonly Dictionary<Type, Type> _registeredRepositoryTypes;
        private bool _isDisposed;


        public UnitOfWork(TContext context, ILog logger)
        {
            _dbContext = context;
            _logger = logger;

            _repositories = new Dictionary<Type, object>();
            _registeredRepositoryTypes = new Dictionary<Type, Type>();
        }


        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            var entityType = typeof(TEntity);

            if (_repositories.TryGetValue(entityType, out var repository))
            {
                return (IRepository<TEntity>)repository;
            }

            if (_registeredRepositoryTypes.TryGetValue(entityType, out var repoType))
            {
                var customRepository = Activator.CreateInstance(repoType, _dbContext, _logger);

                _repositories.Add(entityType, customRepository);

                return (IRepository<TEntity>)customRepository;
            }

            repository = new Repository<TEntity>(_dbContext, _logger);

            _repositories.Add(entityType, repository);

            return (IRepository<TEntity>)repository;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }


        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                _dbContext.Dispose();
            }

            _isDisposed = true;
        }

        protected void RegisterRepositoryType<TEntity, TRepository>() where TEntity : class
           where TRepository : IRepository<TEntity>
        {
            _registeredRepositoryTypes.Add(typeof(TEntity), typeof(TRepository));
        }
    }
}