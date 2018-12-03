# Payroll

Este projeto foi desenvolvido para a obtenção de aprovação na disciplina de TCC na Universidade Estácio.

Tecnologias relacionadas:
<ul>
  <li> C# </li>
  <li> Asp.Net Core </li>
  <li> Sql Server </li>  
</ul>

Este projeto teve como premissas:
<ul>
  <li> Desenvolvimento rápido de aplicações </li>
  <li> Reuso de código </li>
  <li> Abstrações </li>  
  <li> Desenvolvimento centrado no usuário </li>  
</ul>

O projeto está desenvolvido com a arquitetura de 3 camadas (Apresentação, negócio e dados) com responsabilidades bem definidas para cada uma das camadas. Foi também desenvolvido para o reuso de código afim de produzir menos código que faz mais trabalho.

Este projeto consiste em uma aplicação de gerenciamento de Recursos Humanos para uma empresa fictícia que oferece este serviço para outras empresas. Inclue cadastros diversos onde o usuário poderá criar, remover, alterar e consultar os dados de cada entidade da aplicação.

A aplicação está internacionalizada para os seguintes idiomas: pt-BR, en-US, fr-FR. De acordo com o idioma o browser a aplicação irá aplicar o idioma correspondente, caso a aplicação não dê suporte ao idioma solicitado pelo browser continuará com o idioma atual, o padrão é pt-BR. 

Imagens da aplicação: 

Dashboard 
 - Home da aplicação onde o usuário poderá ter acesso a informações rápidas sobre os funcionários gerenciados.
 
  ![Screenshot](dashboard.png)

Telas de Listagens
  - O usuário poderá ter uma visão geral dos registros cadastrados bem como utilizar o filtro para pesquisar registros rapidamente
  
  ![Screenshot](index.png)
 
Telas de Edição
  - O usuário poderá criar novos registros como também editar registros
  
  ![Screenshot](create.png)

Telas de Detalhes
  - O usuário pode ter acesso a todas as informações de um único registro
  
  ![Screenshot](details.png)
  
Telas de exclusão
  - Para o usuário excluir determinado registro
  
  ![Screenshot](remove.png)
  
  Trabalhos futuros (para uma possível v2)
- Dashboard configurável pelo usuário
- Calcular folha de pagamento
- Oferecer opções de tradução diretamente na interface
- Suporte a novos idiomas (es-ES por exemplo)