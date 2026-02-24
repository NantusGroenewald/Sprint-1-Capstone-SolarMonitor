using MediatR;
using SolarMonitor.Application.Repositories;
using SolarMonitor.Domain.Entities;

namespace SolarMonitor.Application.Commands;

public class CreatePanelCommandHandler : IRequestHandler<CreatePanelCommand, Guid>
{
    private readonly IPanelRepository _panelRepository;

    // The Handler gets the repository, NOT the Controller!
    public CreatePanelCommandHandler(IPanelRepository panelRepository)
    {
        _panelRepository = panelRepository;
    }

    public async Task<Guid> Handle(CreatePanelCommand request, CancellationToken cancellationToken)
    {
        var panel = new Panel(request.Brand, request.Model, request.Type);

        await _panelRepository.AddAsync(panel, cancellationToken);

        return panel.Id;
    }
}