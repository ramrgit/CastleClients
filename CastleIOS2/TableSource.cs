using System;
using System.Collections.Generic;
using System.Text;
using Foundation;
using UIKit;
using Edison.Castle.Clients.Data.Models;
using System.Linq;

namespace CastleIOS2
{
    public class TableSource : UITableViewSource
    {

        private List<Lock> LocksList;
        private string CellIdentifier = "LockCell";

        public TableSource(IEnumerable<Lock> locksList)
        {
            LocksList = locksList.ToList<Lock>();

        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
            string item = LocksList[indexPath.Row].LockName + "\n" + LocksList[indexPath.Row].LockUUID.ToString();
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, CellIdentifier);
            }
            cell.TextLabel.Text = LocksList[indexPath.Row].LockName;
            cell.DetailTextLabel.Text = LocksList[indexPath.Row].LockUUID.ToString();

            return cell;
        }
        
        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return LocksList.Count();
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            base.RowSelected(tableView, indexPath);
        }
    }
}
