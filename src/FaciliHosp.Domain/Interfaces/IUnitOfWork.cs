﻿namespace FaciliHosp.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        bool Commit();
    }
}
