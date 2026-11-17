using Microsoft.EntityFrameworkCore;
using ServiTec.Domain.Entities;

namespace ServiTec.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<ClientPhone> ClientPhones => Set<ClientPhone>();
    public DbSet<Device> Devices => Set<Device>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<TicketChangeLog> TicketChangeLogs => Set<TicketChangeLog>();
    public DbSet<TicketStatusHistory> TicketStatusHistories => Set<TicketStatusHistory>();
    public DbSet<TicketAssignment> TicketAssignments => Set<TicketAssignment>();
    public DbSet<Diagnosis> Diagnoses => Set<Diagnosis>();
    public DbSet<Part> Parts => Set<Part>();
    public DbSet<InventoryMovement> InventoryMovements => Set<InventoryMovement>();
    public DbSet<Quote> Quotes => Set<Quote>();
    public DbSet<QuoteItem> QuoteItems => Set<QuoteItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Source for the numeric part of Ticket.Folio (e.g. TK-2026-0001); formatting lives in a service.
        modelBuilder.HasSequence<int>("ticket_folio_seq");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
