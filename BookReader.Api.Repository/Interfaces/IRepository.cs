namespace BookReader.Api.Repository.Interfaces
{
    public interface IRepository<TEntity, VId> where TEntity : class
    {
        /// <summary>
        /// Get all Entities
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <exception cref="OperationCanceledException">Operation canceled exception</exception>
        /// <exception cref="Exceptions.RepositoryException">Repository exception</exception>
        /// <returns>Entities</returns>
        Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get by Id Entity
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <exception cref="OperationCanceledException">Operation canceled exception</exception>
        /// <exception cref="Exceptions.RepositoryException">Repository exception</exception>
        /// <returns>Entity</returns>
        Task<TEntity> GetById(VId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create Entity
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <exception cref="OperationCanceledException">Operation canceled exception</exception>
        /// <exception cref="Exceptions.RepositoryException">Repository exception</exception>
        /// <returns>Entity</returns>
        Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <exception cref="OperationCanceledException">Operation canceled exception</exception>
        /// <exception cref="Exceptions.RepositoryException">Repository exception</exception>
        /// <returns>Entity</returns>
        Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete Entity by Id
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <exception cref="OperationCanceledException">Operation canceled exception</exception>
        /// <exception cref="Exceptions.RepositoryException">Repository exception</exception>
        /// <returns>Entity</returns>
        Task<TEntity> Delete(VId id, CancellationToken cancellationToken = default);
    }
}
