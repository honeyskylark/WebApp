using System.Collections.Generic;

namespace WebApp.Areas.Administration.Constants
{
    public static class ViewConstants
    {
        public static readonly Dictionary<string, string> Resources = new Dictionary<string, string>
        {
            #region CRUD Commands
            { "Change", "Изменить" },
            { "Back", "Назад" },
            { "Home", "Домой" },
            { "Identifier", "Идентификатор" },
            { "DataBase", "База данных" },
            { "ExtendedInformation", "Расширенная информация" },
            { "Adding", "Добавление" },
            { "Add",  "Добавить" },
            { "Delete", "Удалить" },
            { "Edit", "Редактировать" },
            { "Editing", "Редактирование" },
            { "ConfirmDelete", "Подтвердите удаление"},
            { "Actions", "Действия" },
            #endregion

            #region Currencies
            { "Currencies", "Валюты"},
            { "Currency", "Валюта" },
            #endregion

            #region SubSections
            { "SubSections", "Подразделы" },
            { "SubSection", "Подраздел" },
            #endregion

            #region Sections
            { "Sections", "Разделы" },
            { "Section", "Раздел" },
            #endregion

            #region Catalogs
            { "Catalogs", "Каталоги" },
            { "Catalog","Каталог"},
            #endregion

            #region Products
            { "Products", "Продукты"},
            { "Product", "Продукт" },
            { "Price", "Цена" },
            { "Description", "Описание" },
            #endregion

            #region Units
            { "Units", "Единица"},
            { "Unit", "Единицы" },
            #endregion

            #region Roles
            { "Roles", "Роли"},
            { "Role", "Роль" },
            #endregion

            #region Users
            { "Users", "Пользователи"},
            { "User", "Пользователь" },
            { "FirstName", "Имя"},
            { "Patronymic", "Отчество" },
            { "LastName", "Фамилия"},
            { "Login", "Логин" },
            { "Password", "Пароль" },
            #endregion
        };      
    }
}
