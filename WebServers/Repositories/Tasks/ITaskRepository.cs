﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebModels.TaskRequest;

namespace WebServers.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Entities.Task>> GetTaskList(TaskListSearch taskListSearch);
        Task<Entities.Task> GetById(Guid Id);
        Task<Entities.Task> Create(Entities.Task task);
        Task<Entities.Task> Update(Entities.Task task);
        Task<Entities.Task> Delete(Entities.Task task);
    }
}
