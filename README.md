# xamarin-forms-starter
projeto exemplo em xamarin forms com prism

nesse projeto vou utilizar o Visual Studio 2017

vou separar as funcionalidades por branchs

# requisitos do projeto
1. instalar o Visual Studio 2017 (com os pacotes do Xamarin)
2. instalar o template prism (para VS) (https://marketplace.visualstudio.com/items?itemName=BrianLagunas.PrismTemplatePack)
3. colocar a pasta do SKD do Android em uma pasta sem espaços. Por padrão o visual studio instala em c:/arquivos de programas/... e isso pode ocasionar erros de build. Então pegar essa pasta e mover para uma pasta sem espaços (exemplo C:/Andoid). Ir nas configurações do VS, na parte de Xamarin e trocar a pasta que esta apontado o SDK para a pasta que você criou
4. nesse projeto estou utilizando o SKD e API do android 7.1.1 (API 25). Baixe essa API, pois algumas funcionalidades tem que buildar com essa versão
5. ao baixar o projeto, verifique nos pacotes do nuget se precisa atualizar o Xamarin Forms, se sim então atualize
6. recomendo atualizar também no nuget os pacotes da API do android v25.*


# master
será uma primeira versão com uma única alteração que é a implementação do INavigationService e BindableBase que implementa a INotifyPropertyChanged, pois basicamente todas as telas terão efeito sobre essas bibliotecas para funcionarem corretamente

# properties-cache
branch com exemplo de salvar informações no cache da aplicação. Mesmo o usuário encerrando a aplicação ou reiniciando o celular, o valor continuar salvo.

# geolocalizacao
branch com exemplo do uso da geolocalização do disposivito. Nesse exemplo mostro como obter o endereço a partir da localização do dispositivo e como digitar um endereço em uma caixa de texto e o sistema buscar os possíveis endereços que o usuário quis dizer.
```
requisitos dessa branch:

1. instalar o pacote (em todos os projetos portable, droid e ios) Xamarin.Forms.Maps (https://www.nuget.org/packages/Xamarin.Forms.Maps/). Nesse exemplo estou utilizando a versão 2.3.4.267
2. no MainActivity.cs do projeto droid, adicionar no método OnCreate o comando global::Xamarin.FormsMaps.Init(this, bundle);
3. no appDelegate do projeto IOS, adicionar no método FinishedLaunching o mando global::Xamarin.FormsMaps.Init();
4. instalar o pacote (em todos os projetos portable, droid e ios) Xam.Plugin.Geolocator (https://www.nuget.org/packages/Xam.Plugin.Geolocator/). Nesse exemplo estou utilizando a versão 4.0.1.
5. no arquivo manifesto do android, colocar as permissões: ACCESS_COARSE_LOCATION e ACCESS_FINE_LOCATION
6. ao adicionar essas permissões, o Google Play irá filtrar automaticamente dispositivos sem hardware específico. Você pode contornar isso adicionando o seguinte ao seu arquivo AssemblyInfo.cs no seu projeto Android:

[assembly: UsesFeature("android.hardware.location", Required = false)]

[assembly: UsesFeature("android.hardware.location.gps", Required = false)]

[assembly: UsesFeature("android.hardware.location.network", Required = false)]

7. no projeto IOS é preciso das permissões NSLocationAlwaysUsageDescription e RequestWhenInUseAuthorization. Abra o arquivo Info.plist do seu projeto IOS e dentro da tag <dict> colocar as tags:

<key>NSLocationAlwaysUsageDescription</key>

<string>É preciso utilizar a geolocalização.</string>

<key>RequestWhenInUseAuthorization</key>

<string>É preciso utilizar a geolocalização.</string>
```


# bonus
para quem precisa consumir um serviço localhost e está passando por problemas, essa solução é a melhor que encontrei:

> fonte: (Paulo Rogério) http://ilovecode.com.br/xamarin-como-consumir-uma-api-local-que-roda-no-iis-express-no-emulador/

Vamos utilizar o gerenciador de pacotes NPM para instalar o iisexpress-proxy. Para isso iremos precisar de ter o Node.js instalado na máquina.

Para baixar e instalar o Node.JS acesse https://nodejs.org/en/

Após instalar o Node.Js é bem simples, basta instalar o pacote do iisexpress-proxy em nossa máquina.

Para isso abra o prompt do Node.Js em sua máquina e digite.

> npm install -g iisexpress-proxy

Após instalar vamos dar o comando que vai substituir a porta do seu IIS Express para uma porta que o Emulador entenda.

> iisexpress-proxy PortaDaApi to 3000

Agora basta em nosso aplicativo mudar a url de nossa api, ficando assim http://192.168.0.102:3000/api/test/v1/online

Funciona tanto em emulador quanto no device fisico.
