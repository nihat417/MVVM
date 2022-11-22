using Source.Command;
using Source.Models;
using Source.Repositories.Abstracts;
using Source.Stores;
using System;
using System.Windows.Input;

namespace Source.ViewModels;

public class AddViewModel : BaseViewModel
{
    private readonly ICarRepository _carRepository;
    private readonly NavigationStore _navigationStore;




    public Car Car { get; set; } = new Car();

    public ICommand CancelCommand { get; set; }
    public ICommand SubmitCommand { get; set; }




    public AddViewModel(ICarRepository carRepository, NavigationStore navigationStore)
    {
        _carRepository = carRepository;
        _navigationStore = navigationStore;



        CancelCommand = new RelayCommand(ExecuteCancelCommand);
        SubmitCommand = new RelayCommand(ExecuteSubmitCommand , CanExecuteSubmitCommand);
    }

    private bool CanExecuteSubmitCommand(object? obj)
    {
        return Car.Id > 0
            && Car.Make?.Length > 2
            && Car.Model?.Length > 0
            && Car.Year > 1900;
    }

    void ExecuteSubmitCommand(object? parameter)
    {
        _carRepository.Add(Car);
        _navigationStore.CurrentViewModel = new HomeViewModel(_carRepository, _navigationStore);
    }

    void ExecuteCancelCommand(object? parameter)
    {
        _navigationStore.CurrentViewModel = new HomeViewModel(_carRepository, _navigationStore);
    }
}
