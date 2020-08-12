using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VV.Commands;
using VV.CustomElements;

namespace VV.ViewModel
{
    public class MainViewModel
    {
        private VVContext context;
        private RelayCommand addOrderCommand;
        private RelayCommand deleteOrderCommand;
        private RelayCommand addStaffCommand;
        private RelayCommand deleteStaffCommand;
        private RelayCommand addSubdivisionCommand;
        private RelayCommand deleteSubdivisionCommand;
        private RelayCommand saveCommand;
        public List<string> Floor { get; set; } = new List<string> { "Мужской", "Женский" };
        public Order SelectOrder { get; set; }
        public Staff SelectStaff { get; set; }
        public Subdivision SelectSubdivision { get; set; }
        public ObservableCollection<Order> Orders { get; set; }
        public ObservableCollection<Staff> Staffs { get; set; }
        public ObservableCollection<Staff> StaffsForCB { get; set; }
        public ObservableCollection<Subdivision> Subdivisions { get; set; }
        public ObservableCollection<Subdivision> SubdivisionsForCB { get; set; }
        public MainViewModel()
        {
            context = new VVContext();
            try
            {
                context.orders.Load();
                context.staff.Load();
                context.subdivision.Load();
                Orders = context.orders.Local;
                Staffs = context.staff.Local;
                StaffsForCB = new ObservableCollection<Staff>(context.staff.Local.ToList());
                Subdivisions = context.subdivision.Local;
                SubdivisionsForCB = new ObservableCollection<Subdivision>(context.subdivision.Local.ToList());
            } catch
            {
                CustomMessageBox.Show("Ошибка подключения к БД");
                context.Dispose();
            }
        }
        public RelayCommand  AddOrderCommand
        {
            get
            {
                return addOrderCommand ??
                    (addOrderCommand = new RelayCommand (obj =>
                    {
                        Orders.Add(new Order());
                    }));
            }
        }
        public RelayCommand DeleteOrderCommand
        {
            get
            {
                return deleteOrderCommand ??
                    (deleteOrderCommand = new RelayCommand(obj =>
                    {
                        Order order = obj as Order;
                        if (order != null)
                        {
                            CustomMessageBox.ShowYesNo($"Вы действительно хотите удалить заказ №{order.Number}?");
                            if (CustomMessageBox.RESULT == CustomMessageBox.YES)
                            {
                                Orders.Remove(order);
                            }
                        }
                    }));
            }
        }
        public RelayCommand AddStaffCommand
        {
            get
            {
                return addStaffCommand ??
                    (addStaffCommand = new RelayCommand(obj =>
                    {
                        Staffs.Add(new Staff());
                    }));
            }
        }
        public RelayCommand DeleteStaffCommand
        {
            get
            {
                return deleteStaffCommand ??
                    (deleteStaffCommand = new RelayCommand(obj =>
                    {
                        Staff staff = obj as Staff;
                        if(staff != null)
                        {
                            CustomMessageBox.ShowYesNo($"Вы действительно хотите удалить сотрудника {staff.Surname}?");
                            if (CustomMessageBox.RESULT == CustomMessageBox.YES)
                            {
                                Staffs.Remove(staff);
                            }
                        }
                    }));
            }
        }
        public RelayCommand AddSubdivisionCommand
        {
            get
            {
                return addSubdivisionCommand ??
                    (addSubdivisionCommand = new RelayCommand(obj =>
                    {
                        Subdivisions.Add(new Subdivision());
                    }));
            }
        }
        public RelayCommand DeleteSubdivisionCommand
        {
            get
            {
                return deleteSubdivisionCommand ??
                    (deleteSubdivisionCommand = new RelayCommand(obj =>
                    {
                        Subdivision subdivision = obj as Subdivision;
                        if(subdivision != null)
                        {
                            CustomMessageBox.ShowYesNo($"Вы действительно хотите удалить подразделение {subdivision.Name}?");
                            if (CustomMessageBox.RESULT == CustomMessageBox.YES)
                            {
                                Subdivisions.Remove(subdivision);
                            }
                        }
                    }));
            }
        }
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new RelayCommand(obj =>
                    {
                        try
                        {
                            context.SaveChanges();
                            StaffsForCB = new ObservableCollection<Staff>(context.staff.Local.ToList());
                            SubdivisionsForCB = new ObservableCollection<Subdivision>(context.subdivision.Local.ToList());
                            Snackbar snackbar = obj as Snackbar;
                            var messageQueue = snackbar.MessageQueue;
                            var message = "Сохранено";
                            Task.Factory.StartNew(() => messageQueue.Enqueue(message));
                        } catch
                        {
                            CustomMessageBox.Show("Фатальная ошибка при сохранеии!");
                        }
                    }));
            }
        }        
    }
}
