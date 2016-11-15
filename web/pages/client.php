<?php
  header('Access-Control-Allow-Origin: *');
  include "head.php";
  include "navbar.php";
?>

        <div id="page-wrapper" data-id="<?php echo $_GET['id'] ?>">
          <div class="hidden row">
              <div class="col-lg-12">
                  <h1 class="page-header clientName">Loading...</h1>
              </div>
          </div>
          <div class="hidden row">
            <div class="col-lg-6 col-lg-offset-3">
              <table width="100%" class="table table-striped table-bordered table-hover">
                  <thead>
                    <tr>
                      <th colspan="3">Informação do cliente</th>
                    </tr>
                  </thead>
                  <tbody>
                      <tr>
                          <td>Nome</td>
                          <td class="clientName" colspan="2"></td>
                      </tr>
                      <tr>
                          <td>Morada</td>
                          <td id="clientStreet"></td>
                          <td id="clientZip"></td>
                      </tr>
                      <tr>
                          <td>Contacto</td>
                          <td colspan="2">919693255</td>
                      </tr>
                      <tr>
                          <td>Email</td>
                          <td colspan="2">geral@bimbu.pt</td>
                      </tr>
                      <tr>
                          <td>Contribuinte</td>
                          <td id="clientNIF" colspan="2"></td>
                      </tr>
                  </tbody>
              </table>
            </div>
          </div>
          <div class="hidden row">
            <div class="col-lg-6">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <i class="fa fa-archieve fa-fw"></i> Mais comprados
                    </div>
                    <div class="panel-body">
                        <div id="morris-bar-chart"></div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <i class="fa fa-bar-chart-o fa-fw"></i> Evolução
                        <div class="pull-right">
                            <div class="btn-group">
                                <button type="button" class="btn btn-default btn-xs dropdown-toggle" data-toggle="dropdown">
                                    Ações
                                    <span class="caret"></span>
                                </button>
                                <ul class="dropdown-menu pull-right" role="menu">
                                    <li><a href="#">Ano</a>
                                    </li>
                                    <li><a href="#">Mês</a>
                                    </li>
                                    <li><a href="#">Semana</a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div id="morris-area-chart"></div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <i class="fa fa-bar-chart-o fa-fw"></i> Margem de lucro
                    </div>
                    <div class="panel-body">
                        <div id="morris-bar-chart"></div>
                    </div>
                </div>
            </div>
          </div>
        </div>

<?php
  include "footer.php";
?>
