using Source.Command;
using Source.Models;
using Source.Repositories.Abstracts;
using Source.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Source.ViewModels;

public class UpdateViewModel : BaseViewModel
{


    private readonly ICarRepository _carRepository;
    private readonly NavigationStore _navigationStore;
    public Car Car { get; set; } = new Car();

    public ICommand CancelCommand { get; set; }
    public ICommand SubmitCommand { get; set; }


    public UpdateViewModel(ICarRepository carRepository, NavigationStore navigationStore, Car car)
    {
        _carRepository = carRepository;
        _navigationStore = navigationStore;
        Car = car;
        SubmitCommand = new RelayCommand(ExecutSubmitCommand, CanExecutSubmitCommand);
        CancelCommand = new RelayCommand(ExecuteCancelCommand);
    }
    private void ExecutSubmitCommand(object? obj)
    {
        if (obj is UserControl view && view.Content is StackPanel stackPanel)
        {
            foreach (var txt in stackPanel.Children.OfType<TextBox>())
            {
                BindingExpression be = txt.GetBindingExpression(TextBox.TextProperty);
                be.UpdateSource();
            }
        }
        _navigationStore.CurrentViewModel = new HomeViewModel(_carRepository, _navigationStore);
    }

    private bool CanExecutSubmitCommand(object? obj)
    {
        return Car.Id > 0
                && Car.Make?.Length > 2
                && Car.Model?.Length > 0
                && Car.Year > 1900;
    }


    void ExecuteCancelCommand(object? parameter)
    {
        _navigationStore.CurrentViewModel = new HomeViewModel(_carRepository, _navigationStore);
    }










}
