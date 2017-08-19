# xamarin-forms-starter
projeto exemplo em xamarin forms com prism

nesse projeto vou utilizar o Visual Studio 2017

vou separar as funcionalidades por branchs

# master
será uma primeira versão com uma única alteração que é a implementação do INavigationService e BindableBase que implementa a INotifyPropertyChanged, pois basicamente todas as telas terão efeito sobre essas bibliotecas para funcionarem corretamente

# properties-cache
branch com exemplo de salvar informações no cache da aplicação. Mesmo o usuário encerrando a aplicação ou reiniciando o celular, o valor continuar salvo.

# requisitos
1. instalar o Visual Studio 2017 (com os pacotes do Xamarin)
2. instalar o template prism (para VS) (https://marketplace.visualstudio.com/items?itemName=BrianLagunas.PrismTemplatePack)
3. colocar a pasta do SKD do Android em uma pasta sem espaços. Por padrão o visual studio instala em c:/arquivos de programas/... e isso pode ocasionar erros de build. Então pegar essa pasta e mover para uma pasta sem espaços (exemplo C:/Andoid). Ir nas configurações do VS, na parte de Xamarin e trocar a pasta que esta apontado o SDK para a pasta que você criou
4. nesse projeto estou utilizando o SKD e API do android 7.1.1 (API 25). Baixe essa API, pois algumas funcionalidades tem que buildar com essa versão
5. ao baixar o projeto, verifique nos pacotes do nuget se precisa atualizar o Xamarin Forms, se sim então atualize
6. recomendo atualizar também no nuget os pacotes da API do android v25.*
