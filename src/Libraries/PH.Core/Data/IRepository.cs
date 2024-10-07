﻿namespace PH.Core.Data;

public partial interface IRepository<T> where T : BaseEntity
{
    T GetById(object id);
    void Insert(T entity);
    void Update(T entity);
    void Delete(T entity);
    IQueryable<T> GetAll();
}