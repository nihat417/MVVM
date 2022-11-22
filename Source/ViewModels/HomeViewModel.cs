using Source.Command;
using Source.Models;
using Source.Repositories.Abstracts;
using Source.Stores;
using Source.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Source.ViewModels;

public class HomeViewModel : BaseViewModel
{
    private readonly ICarRepository _carRepository;
    private readonly NavigationStore _navigationStore;

    public ObservableCollection<Car> Cars { get; set; }

    public Car? SelectedCar { get; set; }


    public ICommand ShowCommand { get; set; }
    public ICommand AddCommand { get; set; }
    public ICommand UpdateCommand { get; set; }
    public ICommand DeleteCommand { get; set; }



    public HomeViewModel(ICarRepository carRepository, NavigationStore navigationStore)
    {
        _carRepository = carRepository;
        _navigationStore = navigationStore;


        Cars = new(_carRepository.GetList() ?? new List<Car>());

        ShowCommand = new RelayCommand(ExecuteShowCommand, CanExecuteShowCommand);
        AddCommand = new RelayCommand(ExecuteAddCommand);
        UpdateCommand = new RelayCommand(ExecuteUpdateCommand, CanExecuteUpdateCommand);
        DeleteCommand = new RelayCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
    }

    private bool CanExecuteUpdateCommand(object? obj)
        => SelectedCar is not null;

    private bool CanExecuteDeleteCommand(object? obj)
        => SelectedCar is not null;

    private void ExecuteDeleteCommand(object? obj)
    {
        _carRepository.Remove(SelectedCar);
        _navigationStore.CurrentViewModel = new HomeViewModel(_carRepository, _navigationStore);
    }

    private void ExecuteUpdateCommand(object? obj)
    {
        _navigationStore.CurrentViewModel = new UpdateViewModel(_carRepository, _navigationStore,SelectedCar);
    }

    void ExecuteShowCommand(object? parameter)
        => MessageBox.Show(SelectedCar?.Model);


    bool CanExecuteShowCommand(object? parameter)
        => SelectedCar is not null;



    void ExecuteAddCommand(object? parameter)
    {
        _navigationStore.CurrentViewModel = new AddViewModel(_carRepository, _navigationStore);
    }

}
