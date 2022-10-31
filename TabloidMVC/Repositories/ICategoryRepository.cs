﻿using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();

        Category GetCategoryById(int id);

        void AddCategory(Category entry);
        void UpdateCategory(Category entry);
        void DeleteCategory(int id);



    }
}