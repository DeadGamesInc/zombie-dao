namespace ZombieDAO.Repositories; 

public sealed class ProjectsRepository : ZombieDAORepository {
    public ProjectsRepository(IDbContextFactory<DataContext> factory) : base(Log.Logger.ForContext<ProjectsRepository>(), factory, "projects") { }

    public async Task<List<ProjectModel>> GetAll(CancellationToken token) {
        async Task<List<ProjectModel>> action() {
            _logger.Information("Retrieving project list");
            await using var context = await _factory.CreateDbContextAsync(token);
            return await context.Projects.ToListAsync(token);
        }

        return await Executor(action, "get_all");
    }

    public async Task<ProjectModel> GetById(Guid id, CancellationToken token) {
        async Task<ProjectModel> action() {
            _logger.Information("Retrieving project {Id}", id);
            await using var context = await _factory.CreateDbContextAsync(token);
            var project = await context.Projects
                .Include(project => project.Members).ThenInclude(member => member.User)
                .FirstOrDefaultAsync(a => a.ID == id, token);

            if (project == null) throw new NotFoundException("Invalid project ID");
            return project;
        }

        return await Executor(action, "get_by_id");
    }
    
    public async Task<ProjectModel> Create(ProjectModel model, CancellationToken token) {
        async Task<ProjectModel> action() {
            _logger.Information("Creating project {Name}", model.Name);
            await using var context = await _factory.CreateDbContextAsync(token);
            await context.Projects.AddAsync(model, token);
            await context.SaveChangesAsync(token);
            return model;
        }

        return await Executor(action, "create");
    }

    public async Task Update(Guid id, UpdateProjectDTO dto, CancellationToken token) {
        async Task action() {
            _logger.Information("Updating project {Id}", id);
            await using var context = await _factory.CreateDbContextAsync(token);
            var project = await context.Projects.FirstOrDefaultAsync(a => a.ID == id, token);
            if (project == null) throw new NotFoundException("Invalid project ID");
            project.Update(dto);
            context.Projects.Update(project);
            await context.SaveChangesAsync(token);
        }

        await Executor(action, "update");
    }

    public async Task Delete(Guid id, CancellationToken token) {
        async Task action() {
            _logger.Information("Deleting project {Id}", id);
            await using var context = await _factory.CreateDbContextAsync(token);
            var project = await context.Projects.FirstOrDefaultAsync(a => a.ID == id, token);
            if (project == null) throw new NotFoundException("Invalid project ID");
            context.Projects.Remove(project);
            await context.SaveChangesAsync(token);
        }

        await Executor(action, "delete");
    }

    public async Task AddMember(ProjectMemberModel model, CancellationToken token) {
        async Task action() {
            _logger.Information("Adding member to project {Id}", model.ProjectID);
            await using var context = await _factory.CreateDbContextAsync(token);
            await context.ProjectMembers.AddAsync(model, token);
            await context.SaveChangesAsync(token);
        }

        await Executor(action, "add_member");
    }

    public async Task RemoveMember(Guid id, string wallet, CancellationToken token) {
        async Task action() {
            _logger.Information("Removing member from project {Id}", id);
            await using var context = await _factory.CreateDbContextAsync(token);
            var member = await context.ProjectMembers.FirstOrDefaultAsync(a => a.ProjectID == id && a.UserWallet == wallet, token);
            if (member == null) throw new NotFoundException("Unknown project member");
            context.ProjectMembers.Remove(member);
            await context.SaveChangesAsync(token);
        }

        await Executor(action, "remove_member");
    }
}
