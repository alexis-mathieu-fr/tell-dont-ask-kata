using System.Collections.Generic;


public class SellItemsRequest
{
    private List<SellItemRequest> requests;

    public void SetRequests(List<SellItemRequest> requests)
    {
        this.requests = requests;
    }

    public List<SellItemRequest> GetRequests()
    {
        return requests;
    }
}