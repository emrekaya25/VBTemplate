Public Interface IUnitOfWork
    'ReadOnly Property ProductRepository As IProductRepository  / entityler
    Function SaveChangeAsync() As Task(Of Integer)
End Interface
