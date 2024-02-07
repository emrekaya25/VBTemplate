Public Interface IUnitOfWork
    ReadOnly Property ProductRepository As IProductRepository
    ReadOnly Property UserRepository As IUserRepository
    Function SaveChangeAsync() As Task(Of Integer)
End Interface
