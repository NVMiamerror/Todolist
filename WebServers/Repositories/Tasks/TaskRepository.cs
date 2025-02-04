﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebModels.TaskRequest;
using WebServers.Data;

namespace WebServers.Repositories.Tasks
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TodolistDbContext _context;
        public TaskRepository(TodolistDbContext context)
        {
            _context = context;
        }    
        public async Task<IEnumerable<Entities.Task>> GetTaskList(TaskListSearch taskListSearch)
        {
            var query = _context.Tasks.Include(x => x.Assignee).AsQueryable();
            if (!string.IsNullOrEmpty(taskListSearch.Name))
                query = query.Where(x => x.Name.Contains(taskListSearch.Name));

            if (taskListSearch.AssigneeId.HasValue)
                query = query.Where(x => x.AssigneeId == taskListSearch.AssigneeId.Value);

            if (taskListSearch.Priority.HasValue)
                query = query.Where(x => x.Priority == taskListSearch.Priority.Value);
            return await query.OrderBy(x=>x.CreateDate).ToListAsync();
        }
        public async Task<Entities.Task> GetById(Guid Id)
        {
            return await _context.Tasks.FindAsync(Id);
        }
        public async Task<Entities.Task> Create(Entities.Task task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }
        public async Task<Entities.Task> Update(Entities.Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Entities.Task> Delete(Entities.Task task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return task;
        }
    }
}
