namespace ServiTec.Domain.Common;

public static class TextLimits
{
    /// <summary>
    /// Longitud maxima, en caracteres de textos largos, como la falla reportada o la condicion fisica.
    /// Se usa en la capa Application en los DTOs y la capa Infrastructure en BaseEntityConfiguration.
    /// Si el valor cambia, las columnas de la base cambian de tamaño y hay que crear una migracion.
    /// </summary>
    public const int LongTextMaxLength = 2000;
}