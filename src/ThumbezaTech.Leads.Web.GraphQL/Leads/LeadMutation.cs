using HotChocolate.Subscriptions;

using Mediator;

using ThumbezaTech.Leads.Application.Leads;
using ThumbezaTech.Leads.Domain.LeadAggregate;
using ThumbezaTech.Leads.Domain.LeadAggregate.Events;

namespace ThumbezaTech.Leads.Web.GraphQL.Leads;

[ExtendObjectType(OperationTypeNames.Mutation)]
internal sealed class LeadMutation
{
  [GraphQLName("create_lead")]
  [GraphQLDescription("Create a lead")]
  public async Task<string> CreateLead(
      [Service] ISender Sender,
      [Service] ITopicEventSender topicSender,
      [GraphQLNonNullType] LeadVm lead,
      CancellationToken cancellationToken = default)
  {
    var result = await Sender.Send(
      new CreateLeadCommand(lead.FirstName, lead.LastName, lead.DateOfBirth, lead.EmailAddress, lead.MobileNumber, lead.Address),
      cancellationToken);
    if (!result.IsSuccess)
    {
      return string.Join("; ", result.Errors);
    }

    await topicSender.SendAsync("OnLeadPublishedTopic", lead, cancellationToken);
    return result.SuccessMessage;
  }

  [GraphQLName("update_lead")]
  [GraphQLDescription("Update a lead")]
  public async Task<string> UpdateLead(
      [Service] ISender Sender,
      [Service] ITopicEventSender topicSender,
      [GraphQLNonNullType] LeadVm lead,
      CancellationToken cancellationToken = default)
  {
    var result = await Sender.Send(new UpdateLeadCommand((Lead)lead), cancellationToken);
    if (!result.IsSuccess)
    {
      return string.Join("; ", result.Errors);
    }

    await topicSender.SendAsync("OnLeadPublishedTopic", lead, cancellationToken);
    return result.SuccessMessage;
  }

  [GraphQLName("activate_lead")]
  [GraphQLDescription("Activate a lead")]
  public async Task ActivateLead(
      [Service] IPublisher Sender,
      [Service] ITopicEventSender topicSender,
      [GraphQLNonNullType] string activationId,
      CancellationToken cancellationToken = default)
  {
    await Sender.Publish(new LeadActivedEvent(activationId), cancellationToken);
  }

  [GraphQLName("deactivate_lead")]
  [GraphQLDescription("De-activate a lead")]
  public async Task DeactivateLead(
      [Service] IPublisher Sender,
      [Service] ITopicEventSender topicSender,
      [GraphQLNonNullType] string activationId,
      CancellationToken cancellationToken = default)
  {
    await Sender.Publish(new LeadAbandonedEvent(activationId), cancellationToken);
  }

}
