﻿using FIAP.IRRIGACAO.API.Models;

namespace FIAP.IRRIGACAO.API.Repository.Interfaces
{
    public interface ILocationRepository
    {
        IEnumerable<LocationModel> GetAll();
    }
}