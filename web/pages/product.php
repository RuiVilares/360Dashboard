<?php
  header('Access-Control-Allow-Origin: *');
  include "head.php";
  include "navbar.php";
?>


        <div id="page-wrapper" data-id="<?php echo $_GET['id'] ?>">
          <div class="row">
              <div class="col-lg-12">
                  <h1 class="page-header productName">Loading...</h1>
              </div>
          </div>
          <div class="hidden row">
            <div class="col-lg-4 col-md-6">
                <div class="panel panel-green">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-3">
                                <i class="fa fa-bar-chart-o fa-4x"></i>
                            </div>
                            <div class="col-xs-9 text-right">
                                <div class="h1">20 %</div>
                                <div class="h3">Margem de lucro</div>
                            </div>
                        </div>
                    </div>
                    <a href="#">
                        <div class="panel-footer">
                            <span class="pull-left">Ver detalhes</span>
                            <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                            <div class="clearfix"></div>
                        </div>
                    </a>
                </div>
            </div>
            <div class="col-lg-4 col-md-6">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-3">
                                <i class="fa fa-eur fa-4x"></i>
                            </div>
                            <div class="col-xs-9 text-right">
                                <div class="h1">1,25 €</div>
                                <div class="h3">Custo de compra</div>
                            </div>
                        </div>
                    </div>
                    <a href="#">
                        <div class="panel-footer">
                            <span class="pull-left">Ver detalhes</span>
                            <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                            <div class="clearfix"></div>
                        </div>
                    </a>
                </div>
            </div>
            <div class="col-lg-4 col-md-6">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-3">
                                <i class="fa fa-eur fa-4x"></i>
                            </div>
                            <div class="col-xs-9 text-right">
                                <div class="h1">1,5 €</div>
                                <div class="h3">PVP</div>
                            </div>
                        </div>
                    </div>
                    <a href="#">
                        <div class="panel-footer">
                            <span class="pull-left">Ver detalhes</span>
                            <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                            <div class="clearfix"></div>
                        </div>
                    </a>
                </div>
            </div>
          </div>
          <div class="hidden row">
            <div class="col-lg-6 col-lg-offset-3">
              <table width="100%" class="table table-striped table-bordered table-hover">
                  <thead>
                    <tr>
                      <th colspan="4">Informação do produto</th>
                    </tr>
                  </thead>
                  <tbody>
                      <tr>
                          <td>Nome</td>
                          <td class="productName"></td>
                      </tr>
                      <tr>
                          <td>Preço</td>
                          <td>1,5€</td>
                      </tr>
                      <tr>
                          <td>Quantidade em stock</td>
                          <td>8</td>
                      </tr>
                      <tr>
                          <td>Unidades vendidas</td>
                          <td>11</td>
                      </tr>
                      <tr>
                          <td>Margem de lucro</td>
                          <td>20%</td>
                      </tr>
                  </tbody>
              </table>
            </div>
          </div>
          <div class="hidden row">
            <div class="col-lg-6">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <i class="fa fa-users fa-fw"></i> Melhores clientes
                    </div>
                    <div class="panel-body">
                        <div id="morris-bar-chart"></div>
                    </div>
                </div>
            </div>
              <div class="col-lg-6">
                <table width="100%" class="table table-striped table-bordered table-hover">
                    <thead>
                      <tr>
                        <th>Nome</th>
                        <th>Quantidade</th>
                        <th>Cliente</th>
                      </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>Nome</td>
                            <td>Quantidade</td>
                            <td>Cliente</td>
                        </tr>
                        <tr>
                            <td>Nome</td>
                            <td>Quantidade</td>
                            <td>Cliente</td>
                        </tr>
                        <tr>
                            <td>Nome</td>
                            <td>Quantidade</td>
                            <td>Cliente</td>
                        </tr>
                    </tbody>
                </table>
              </div>
          </div>
        </div>

<?php
  include "footer.php";
?>
