using Dapper;
using DemoApplication.BusinessEntity;
using DemoApplication.Repository.Interface;
using Microsoft.Extensions.Options;

namespace DemoApplication.Repository
{
    public class ToDoRepository : BaseRepository, IToDoRepository
    {
        public ToDoRepository(IOptions<ConnectionSettings> Configs) : base(Configs)
        {

        }

        #region Get TODO

        /// <summary>
        /// Get All ToDoItems
        /// </summary>
        /// <returns></returns>
        public List<ToDo> GetAllToDoItems()
        {
            return Query<ToDo>("usp_getalltodoItem", new DynamicParameters());
        }

        /// <summary>
        /// Get ToDO By ItemsId
        /// </summary>
        /// <returns></returns>
        public ToDo GetToDoItemByToDoId(int todoId)
        {
            var dynamicParameters = new DynamicParameters();
            var args = new Dictionary<string, object>()
            {
                ["todoId"] = todoId
            };
            dynamicParameters.AddDynamicParams(args);
            return QueryFirstOrDefault<ToDo>("usp_gettodoitemsbytodoid", dynamicParameters);
        }

        #endregion

        #region Manage ToDo Add/Update/Delete

        public ToDo ManageToDo(ToDo obj)
        {
            var dynamicParameters = new DynamicParameters();
            var args = new Dictionary<string, object>()
            {
                ["id"] = obj.Id,
                ["todoitems"] = obj.ToDoItems,
                ["assignduedates"] = obj.AssignDueDates,
                ["comments"] = obj.Comments,
                ["iscompleted"] = obj.IsCompleted,
                ["userid"] = obj.UserId
            };
            dynamicParameters.AddDynamicParams(args);
            return QueryFirstOrDefault<ToDo>("usp_managetodo", dynamicParameters);
        }

        #endregion
    }
}
