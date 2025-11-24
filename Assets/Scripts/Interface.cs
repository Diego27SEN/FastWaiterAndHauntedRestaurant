using System.Collections.Generic;

public interface IShowDialog
{
    void ShowDialog();
}
public interface IInteractable
{
    void Interact();
}
public interface IEnemyEffect
{
    void ApplyDebuff();
}
public interface IDeliverFood
{
    public void deliverFood(string pedido);
}
public interface IGestionCronometro
{
    public void GestionCronometro();
}