namespace CollectionExtensionsLibrary
{
    /// <summary>
    /// Especifica a direção de navegação para paginação baseada em keyset (seek method).
    /// </summary>
    public enum SeekDirection
    {
        /// <summary>
        /// Avança para a próxima página (valores maiores que o cursor).
        /// </summary>
        Forward,

        /// <summary>
        /// Retrocede para a página anterior (valores menores que o cursor).
        /// </summary>
        Backward
    }
}

