using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace Program.Objects.Stylist_Clients
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };

      Get["/stylists"] = _ => {
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["stylists.cshtml", AllStylists];
      };

      Post["/stylists/new"] = _ => {
        var newStylist = new Stylist(Request.Form["stylist-name"]);
        newStylist.Save();
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["stylists.cshtml", AllStylists];
      };

      Get["/stylists/{id}"] = parameters => {
        var model = new Dictionary<string, object>();
        var selecetedStylist = Stylist.Find(parameters.id);
        List<Client> Stylistclient = selecetedStylist.GetClients();
        var AllStylists = Stylist.GetAll();
        model.Add("stylist", selecetedStylist);
        model.Add("clients", Stylistclient);
        model.Add("stylists", AllStylists);
        return View["clients.cshtml", model];
      };

      Post["/stylists/{id}/new"] = parameters => {
        var newClient = new Client(Request.Form["name"], Request.Form["stylist-id"]);
        newClient.Save();

        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylist SelectedStylist = Stylist.Find(parameters.id);
        List<Client> StylistClient = SelectedStylist.GetClients();
        List<Stylist> AllStylists = Stylist.GetAll();
        model.Add("stylist", SelectedStylist);
        model.Add("clients", StylistClient);
        model.Add("stylists", AllStylists);
        return View["clients.cshtml", model];
      };

      Get["/stylist/edit/{id}"] = parameters => {
        Stylist SelectedStylist = Stylist.Find(parameters.id);
        return View["stylist_edit.cshtml", SelectedStylist];
      };


      Patch["/stylist/edit/{id}"] = parameters => {
        Stylist SelectedStylist = Stylist.Find(parameters.id);
        SelectedStylist.Update(Request.Form["stylist-name"]);

        List<Stylist> AllStylists = Stylist.GetAll();
        return View["stylists.cshtml", AllStylists];
      };

      Delete["/stylist/delete/{id}"] = parameters => {
        Stylist SelectedStylist = Stylist.Find(parameters.id);
        SelectedStylist.Delete();

        List<Stylist> AllStylists = Stylist.GetAll();
        return View["stylists.cshtml", AllStylists];
      };

      Get["/client/edit/{id}"] = parameters => {
        Client SelectedClient = Client.Find(parameters.id);
        return View["client_edit.cshtml", SelectedClient];
      };

      Patch["/client/edit/{id}"] = parameters => {
        Client SelectedClient = Client.Find(parameters.id);
        SelectedClient.Update(Request.Form["client-name"]);

        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylist SelectedStylist = Stylist.Find(SelectedClient.GetStylistId());
        List<Client> StylistClient = SelectedStylist.GetClients();
        List<Stylist> AllStylists = Stylist.GetAll();
        model.Add("stylist", SelectedStylist);
        model.Add("clients", StylistClient);
        model.Add("stylists", AllStylists);
        return View["clients.cshtml", model];
      };

      Delete["/client/delete/{id}"] = parameters => {
        Client SelectedClient = Client.Find(parameters.id);
        SelectedClient.Delete();

        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylist SelectedStylist = Stylist.Find(SelectedClient.GetStylistId());
        List<Client> StylistClient = SelectedStylist.GetClients();
        List<Stylist> AllStylists = Stylist.GetAll();
        model.Add("stylist", SelectedStylist);
        model.Add("clients", StylistClient);
        model.Add("stylists", AllStylists);
        return View["clients.cshtml", model];
      };
    }
  }
}
