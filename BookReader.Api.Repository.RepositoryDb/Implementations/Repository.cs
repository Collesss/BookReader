using BookReader.Api.Repository.Exceptions;
using BookReader.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookReader.Api.Repository.RepositoryDb.Implementations
{
    public abstract class Repository<TEntity, VId, EDbContext> : IRepository<TEntity, VId>
        where TEntity : class
        where EDbContext : DbContext
    {
        private readonly ILogger<Repository<TEntity, VId, EDbContext>> _logger;
        protected readonly EDbContext _dbContext;

        protected Repository(ILogger<Repository<TEntity, VId, EDbContext>> logger, EDbContext dbContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        virtual protected async Task<TEntity> Find(VId id, CancellationToken cancellationToken = default) =>
            await _dbContext.FindAsync<TEntity>(id, cancellationToken);

        public async Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken = default)
        {
            TEntity result;

            try
            {
                _logger.LogTrace($"Adding entity ${entity}.");

                result = (await _dbContext.AddAsync(entity, cancellationToken)).Entity;
                await _dbContext.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Entity added: {result}.");
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error adding entity: {entity}.");

                throw new RepositoryException($"Error adding entity: {entity}. See inner exception.", e);
            }

            return result;
        }

        public async Task<TEntity> Delete(VId id, CancellationToken cancellationToken = default)
        {
            TEntity result;

            try
            {
                _logger.LogTrace($"Remove entity with Id: ${id}.");

                TEntity entity = await Find(id, cancellationToken);

                result = _dbContext.Remove(entity).Entity;
                await _dbContext.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Entity removed: {result}.");
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error remove entity with Id: {id}.");

                throw new RepositoryException($"Error remove entity with Id: {id}. See inner exception.", e);
            }

            return result;
        }

        public async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default) =>
            await _dbContext.Set<TEntity>().ToListAsync(cancellationToken);

        public async Task<TEntity> GetById(VId id, CancellationToken cancellationToken = default)
        {
            TEntity result;

            try
            {
                _logger.LogTrace($"Find entity with Id: ${id}.");

                result = await Find(id, cancellationToken);

            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error find entity with Id: {id}.");

                throw new RepositoryException($"Error find entity with Id: {id}. See inner exception.", e);
            }

            return result;
        }

        public async Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            TEntity result;

            try
            {
                _logger.LogTrace($"Updating entity ${entity}.");

                result = _dbContext.Update(entity).Entity;
                await _dbContext.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Entity updated: {result}.");
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error update entity: {entity}.");

                throw new RepositoryException($"Error update entity: {entity}. See inner exception.", e);
            }

            return result;
        }
    }
}
