#Componentes instalados
<pre>
    -- dotnet add package Microsoft.EntityFrameworkCore --version 6.0.15
    -- dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.15
    -- dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 6.0.15
    -- dotnet add package Microsoft.EntityFrameworkCore.Tools --version 6.0.15
    -- dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 6.0.13
    -- dotnet add package Newtonsoft.Json --version 13.0.3
    -- dotnet add package Microsoft.AspNetCore.Authentication --version 2.2.0
    -- dotnet add package Microsoft.AspNetCore.Authentication.Core --version 2.2.0
    -- dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 6.0.16
</pre>

#Comandos do migration
<pre>
    -- dotnet ef migrations add EstruturaInicial #Usado para a criação do admin
    -- dotnet ef database update
</pre>

#Comandos geração das views de tela administrador
<pre>
    -- dotnet aspnet-codegenerator controller -name AdminController -m AdminModel -dc BancoContext --relativeFolderPath Controllers --useDefaultLayout
    -- dotnet aspnet-codegenerator controller -name CadastroController -m CadastroModel -dc BancoContext --relativeFolderPath Controllers --useDefaultLayout    
    -- dotnet aspnet-codegenerator controller -name ContatoController -m ContatoModel -dc BancoContext --relativeFolderPath Controllers --useDefaultLayout    
    -- dotnet aspnet-codegenerator controller -name EnderecoController -m EnderecoModel -dc BancoContext --relativeFolderPath Controllers --useDefaultLayout        
    -- dotnet aspnet-codegenerator controller -name ObservacaoController -m ObservacaoModel -dc BancoContext --relativeFolderPath Controllers --useDefaultLayout
            
    -- dotnet aspnet-codegenerator controller -name ProfissaoController -m ProfissaoModel -dc BancoContext --relativeFolderPath Controllers --useDefaultLayout
</pre>